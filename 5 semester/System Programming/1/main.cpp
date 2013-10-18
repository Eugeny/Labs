#include "stdio.h"
#include "stdlib.h"
#include "string.h"
#include <vector>
#include <iostream>
#include <string>

#include "fat.h"

#define SECTOR_SIZE 512

FILE* disk;
fat_bs_t  *fat_boot;
u32int first_data_sector;
u32int first_fat_sector;
u32int *fat_table;
 

void read(int lba, void* buffer) {
	fseek(disk, lba * SECTOR_SIZE, SEEK_SET);
	fread(buffer, SECTOR_SIZE, 1, disk);
}

u32int getFATValue(u32int cluster) {
    u32int cls = fat_boot->bytes_per_sector;
    u32int fat_offset = cluster * 4;
    u32int fat_sector = first_fat_sector + (fat_offset / cls);
    u32int ent_offset = fat_offset % cls;
    read(fat_sector, fat_table);
    return fat_table[ent_offset/4] & 0x0FFFFFFF;
}

void readFile(u32int cluster, void* buffer) {
    u32int fatval = cluster;
    do {
        cluster = fatval;
        fatval = getFATValue(cluster);
        for (int i = 0; i < fat_boot->sectors_per_cluster; i++) {
        	read(first_data_sector+(cluster-2)*fat_boot->sectors_per_cluster + i, buffer);
            buffer += 512;
        }
    } while (fatval < 0x0FFFFFF7 && fatval > 0);
}

fat_node *parseDir(void *data, u32int size, u32int *len) {
    u32int offset = 0;
    if (*(u8int*)data == 0) return NULL;

    while (offset < size && (*(u8int*)data == 0x2e)) {
        offset += 0x20;
        data += 0x20;
    }

    if (offset >= size) return NULL;

    fat_file_t* file = (fat_file_t*)data;
    fat_node* node = (fat_node*)malloc(sizeof(fat_node));
    node->name[255] = 0;
    node->nameptr = &(node->name[255]);
    node->dead = (*(u8int*)data == 0xe5);

    while (file->attr == 0x8) {
        offset += 0x20;
        data += 0x20;
        if (offset == size) return NULL;
        file = (fat_file_t*)data;
    }

    while (file->attr == 0xF) {
        fat_lname_t* name = (fat_lname_t*)data;
        char* cptr;

        int offsets[] = { 1, 3, 5, 7, 9, 14, 16, 18, 20, 22, 24, 28, 30 };

        for (int i = 12; i >= 0; i--) {
            cptr = (char*)((u32int)file + offsets[i]);
            if (*cptr <= 0 || *cptr >= 0xFF) continue;
            node->nameptr--;
            *(node->nameptr) = *cptr;
        }

        offset += 0x20;
        data += 0x20;
        if (offset == size) return NULL;
        file = (fat_file_t*)data;
    }

    if (*(u8int*)data == 0)
        return NULL;
    if (*node->nameptr == 0) {
        u8int* p = (u8int*)file->dosfn;
        node->nameptr = node->name;
        for (int i = 0; i < 11; i++) {
            if (*p != 0x20 && *p != 0)
                *node->nameptr++ = *p;
            if (i == 7)
                *node->nameptr++ = '.';
            p++;
        }
        *node->nameptr++ = 0;
        node->nameptr = node->name;
    }
    node->attr = file->attr;
    node->size = file->size;
    node->cluster = file->ch * 65536 + file->cl;
    offset += 0x20;
    *len = offset;
    return node;
}

std::vector<fat_node*> *listFiles(u32int cls) {
    std::vector<fat_node*> *list = new std::vector<fat_node*>();
    void* buf = malloc(512*1024);
    readFile(cls, buf);
    void* ptr = buf;
    while (true) {
        u32int offset = 0;
        fat_node* node = parseDir(ptr, 512*100, &offset);
        if (!node) {
            return list;
        }
        if (strlen(node->nameptr) > 0) 
            list->push_back(node);
        else
        	delete node;

        ptr += offset;
    }

    return list;
}


void recover_file(fat_node* node) {
	void* buffer = malloc(1024*10);
	int clusters = node->size / SECTOR_SIZE + 1;
	int recovered = 0;
	std::cout << " CLUSTERS ";
	for (int i = 0; i < clusters; i++) {
		int cluster = node->cluster + i + first_data_sector - 2;
		printf(" %u ", first_data_sector);
		std::cout << cluster << " ";
		if (getFATValue(cluster) != 0)
			break;
		read(cluster, (buffer+i*SECTOR_SIZE));
		recovered += SECTOR_SIZE;
	}
	if (recovered < node->size) {
		std::cout << " PARTIALLY RECOVERED ";
	} else {
		std::cout << " FULLY RECOVERED ";
		recovered = node->size;
	}
	FILE* out = fopen(node->nameptr, "w");
	fwrite(buffer, recovered, 1, out);
	fclose(out);
}

void recover_rec(u32int cluster, int depth) {
    std::vector<fat_node*>* list = listFiles(cluster);
    for (int i = 0; i < list->size(); i++) {
    	fat_node* node = (*list)[i];
    	for (int j = 0; j < depth * 3; j++)
    		std::cout << " ";
    	std::cout << node->nameptr;
    	if (node->attr & 0x10)
    		std::cout << " [dir] ";
    	else {
    		std::cout << " [file] " << node->size << " bytes ";
    	}
    	if (node->dead) {
    		std::cout << " DELETED; ";
    		recover_file(node);
    	}
    	std::cout << std::endl;
    	if (node->attr & 0x10)
    		recover_rec(node->cluster, depth + 1);
    	delete node;
    }
	delete list;
}

int main() {
	disk = fopen("image", "rb");

    fat_boot = (fat_bs_t*)malloc(SECTOR_SIZE);
    read(0, fat_boot);

    first_data_sector = fat_boot->reserved_sector_count + (fat_boot->table_count * fat_boot->table_size_32);
    first_fat_sector = fat_boot->reserved_sector_count;

    fat_table = (u32int*)malloc(SECTOR_SIZE);

    printf("FAT: %i tables, %u sectors\n", fat_boot->table_count, fat_boot->total_sectors_32);

    recover_rec(fat_boot->root_cluster, 0);

	return 0;
} 

