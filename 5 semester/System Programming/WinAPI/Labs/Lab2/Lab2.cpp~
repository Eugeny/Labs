#include "stdafx.h"
#include "Lab2.h"

HINSTANCE hInst;				
HWND hAddButton, hTextbox, hDeleteButton, hMoveButton, hClearButton, hList1, hList2;

TCHAR* szTitle = _T("Lab 2");				
TCHAR* szWindowClass = _T("Lab2");

ATOM				RegisterWindowClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);

int APIENTRY _tWinMain(_In_ HINSTANCE hInstance,
					   _In_opt_ HINSTANCE hPrevInstance,
					   _In_ LPTSTR    lpCmdLine,
					   _In_ int       nCmdShow)
{
	RegisterWindowClass(hInstance);

	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}

	MSG msg;
	while (GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return (int) (msg.wParam);
}


ATOM RegisterWindowClass(HINSTANCE hInstance)
{
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style			= CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc	= WndProc;
	wcex.cbClsExtra		= 0;
	wcex.cbWndExtra		= 0;
	wcex.hInstance		= hInstance;
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_LAB2));
	wcex.hCursor		= LoadCursor(hInstance, MAKEINTRESOURCE(IDC_LAB2));
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_LAB2);
	wcex.lpszClassName	= szWindowClass;
	wcex.hIconSm		= LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_LAB2));

	return RegisterClassEx(&wcex);
}

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	HWND hWnd;

	hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, NULL, NULL, hInstance, NULL);

	hList1 = CreateWindowEx(WS_EX_CLIENTEDGE
		, L"LISTBOX", NULL
		, WS_CHILD | WS_VISIBLE | WS_VSCROLL | ES_AUTOVSCROLL | LBS_MULTIPLESEL 
		, 10, 10, 300, 300
		, hWnd, NULL, hInstance, NULL);

	hList2 = CreateWindowEx(WS_EX_CLIENTEDGE
		, L"LISTBOX", NULL
		, WS_CHILD | WS_VISIBLE | WS_VSCROLL | ES_AUTOVSCROLL | LBS_MULTIPLESEL 
		, 320, 10, 300, 300
		, hWnd, NULL, hInstance, NULL);

	hAddButton = CreateWindowEx(WS_EX_APPWINDOW
		, L"BUTTON", NULL
		, WS_CHILD | WS_VISIBLE
		, 10, 320, 100, 40
		, hWnd, (HMENU)1, hInstance, NULL);        
	SetWindowText(hAddButton, L"Add");

	hTextbox = CreateWindowEx(WS_EX_CLIENTEDGE
		, L"EDIT", NULL
		, WS_CHILD | WS_VISIBLE | ES_AUTOVSCROLL
		, 120, 320, 100, 40
		, hWnd, (HMENU)2, hInstance, NULL);

	hDeleteButton = CreateWindowEx(WS_EX_APPWINDOW
		, L"BUTTON", NULL
		, WS_CHILD | WS_VISIBLE
		, 240, 320, 100, 40
		, hWnd, (HMENU)3, hInstance, NULL);        
	SetWindowText(hDeleteButton, L"Delete");

	hClearButton = CreateWindowEx(WS_EX_APPWINDOW
		, L"BUTTON", NULL
		, WS_CHILD | WS_VISIBLE
		, 360, 320, 100, 40
		, hWnd, (HMENU)4, hInstance, NULL);        
	SetWindowText(hClearButton, L"Clear");

	hMoveButton = CreateWindowEx(WS_EX_APPWINDOW
		, L"BUTTON", NULL
		, WS_CHILD | WS_VISIBLE
		, 480, 320, 100, 40
		, hWnd, (HMENU)5, hInstance, NULL);        
	SetWindowText(hMoveButton, L"To Right");


	MoveWindow(hWnd, 0, 0, 640, 440, FALSE);
	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	return TRUE;
}


LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;
	PAINTSTRUCT ps;
	HDC hdc;

	switch (message)
	{
	case WM_COMMAND:
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		switch (wmId)
		{
		case 1: {
			char *text = new char[256];
			GetWindowTextA(hTextbox, (LPSTR)text, 256);
			SetWindowText(hTextbox, (LPWSTR)"");
	
			while (text[strlen(text)-1] == 32) {
				text[strlen(text)-1] = 0;
			}
			while (*text == 32)
				text++;

			bool match = FALSE;
			int count = SendMessageA(hList1, LB_GETCOUNT, 0, 0);
			for (int i = 0; i < count; i++) {
				char* data = (char*)SendMessageA(hList1, LB_GETITEMDATA, (WPARAM)i, 0);
				if (strcmp(data, text) == 0)
					match = TRUE;
			}

			if (!match && strlen(text) > 0) {
				int idx = SendMessageA(hList1, LB_ADDSTRING, 0, (LPARAM)text);
				SendMessageA(hList1, LB_SETITEMDATA, idx, (LPARAM)text);
			}
			break;
				}
		case 3: {
			int count = SendMessageA(hList1, LB_GETSELCOUNT, 0, 0);
			int *buf = new int[count];
			SendMessageA(hList1, LB_GETSELITEMS, (WPARAM)count, (LPARAM)buf);
			for (int i = count - 1; i >= 0; i--) {
				SendMessageA(hList1, LB_DELETESTRING, buf[i], 0);
			}
			delete buf;

			count = SendMessageA(hList2, LB_GETSELCOUNT, 0, 0);
			buf = new int[count];
			SendMessageA(hList2, LB_GETSELITEMS, (WPARAM)count, (LPARAM)buf);
			for (int i = count - 1; i >= 0; i--) {
				SendMessageA(hList2, LB_DELETESTRING, i, buf[i]);
			}
			delete buf;
			break;
				}
		case 4: {
			int count = SendMessageA(hList1, LB_GETCOUNT, 0, 0);
			for (int i = count-1; i >= 0; i--)
				SendMessageA(hList1, LB_DELETESTRING, 0, 0);
			count = SendMessageA(hList2, LB_GETCOUNT, 0, 0);
			for (int i = count-1; i >= 0; i--)
				SendMessageA(hList2, LB_DELETESTRING, 0, 0);
			break;
				}
		case 5: {
			int count = SendMessageA(hList1, LB_GETSELCOUNT, 0, 0);
			int *buf = new int[count];
			SendMessageA(hList1, LB_GETSELITEMS, (WPARAM)count, (LPARAM)buf);
			for (int i = 0; i < count; i++) {
				int data = SendMessageA(hList1, LB_GETITEMDATA, (WPARAM)buf[i], 0);
				char* s = strdup((char*)data);
				int idx = SendMessageA(hList2, LB_ADDSTRING, 0, (LPARAM)s);
				SendMessageA(hList2, LB_SETITEMDATA, idx, (LPARAM)s);
			}
			delete buf;
			break;
				}
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		break;
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}
