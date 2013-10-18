typedef unsigned long  u64int;
typedef          long  s64int;
typedef unsigned int   u32int;
typedef          int   s32int;
typedef unsigned short u16int;
typedef          short s16int;
typedef unsigned char  u8int;
typedef signed   char  s8int;

#define PACKED __attribute__((packed))

typedef struct {
	unsigned char 		bootjmp[3];
	unsigned char 		oem_name[8];
	unsigned short 	        bytes_per_sector;
	unsigned char		sectors_per_cluster;
	unsigned short		reserved_sector_count;
	unsigned char		table_count;
	unsigned short		root_entry_count;
	unsigned short		total_sectors_16;
	unsigned char		media_type;
	unsigned short		table_size_16;
	unsigned short		sectors_per_track;
	unsigned short		head_side_count;
	unsigned int 		hidden_sector_count;
	unsigned int 		total_sectors_32;
	unsigned int		table_size_32;
	unsigned short		extended_flags;
	unsigned short		fat_version;
	unsigned int		root_cluster;
	unsigned short		fat_info;
	unsigned short		backup_BS_sector;
	unsigned char 		reserved_0[12];
	unsigned char		drive_number;
	unsigned char 		reserved_1;
	unsigned char		boot_signature;
	unsigned int 		volume_id;
	unsigned char		volume_label[11];
	unsigned char		fat_type_label[8];
} PACKED fat_bs_t;

typedef struct {
    char    dosfn[11];
    u8int   attr;
    u8int   reserved_0;
    u8int   ctime[5];
    u8int   atime[2];
    u16int  ch;
    u8int   mtime[4];
    u16int  cl;
    u32int  size;
} PACKED fat_file_t;

typedef struct {
    u8int   seq;
    char   text1[10];
    u16int  reserved_0;
    u8int   checksum;
    char   text2[12];
    u16int  zero;
    char   text3[4];
} PACKED fat_lname_t;


typedef struct {
    char    name[256];
    char   *nameptr;
    u8int   attr;
    u32int  cluster;
    u32int  size;
    int     dead;
} fat_node;