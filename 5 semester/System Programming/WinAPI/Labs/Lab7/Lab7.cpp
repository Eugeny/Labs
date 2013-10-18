#include "stdafx.h"
#include "Lab7.h"

HINSTANCE hInst;				
HWND hList1, hList2;

TCHAR* szTitle = _T("Lab 7");				
TCHAR* szWindowClass = _T("Lab7");

ATOM				RegisterWindowClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
LPWSTR GetPriorityString(int);
void Reload();

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
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_LAB7));
	wcex.hCursor		= LoadCursor(hInstance, MAKEINTRESOURCE(IDC_LAB7));
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_LAB7);
	wcex.lpszClassName	= szWindowClass;
	wcex.hIconSm		= LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_LAB7));

	return RegisterClassEx(&wcex);
}

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	HWND hWnd;

	hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, NULL, NULL, hInstance, NULL);

	hList1 = CreateWindowEx(WS_EX_CLIENTEDGE
		, L"LISTBOX", NULL
		, WS_CHILD | WS_VISIBLE | WS_VSCROLL | ES_AUTOVSCROLL | LBS_NOTIFY
		, 10, 10, 300, 300
		, hWnd, (HMENU)1, hInstance, NULL);

	hList2 = CreateWindowEx(WS_EX_CLIENTEDGE
		, L"LISTBOX", NULL
		, WS_CHILD | WS_VISIBLE | WS_VSCROLL | ES_AUTOVSCROLL
		, 320, 10, 300, 300
		, hWnd, (HMENU)2, hInstance, NULL);



	MoveWindow(hWnd, 0, 0, 640, 440, FALSE);
	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	Reload();
	return TRUE;
}

void Reload() {
	HANDLE hProcessSnap;
	HANDLE hProcess;
	PROCESSENTRY32 pe32;
	DWORD dwPriorityClass;

	// Take a snapshot of all processes in the system.
	hProcessSnap = CreateToolhelp32Snapshot( TH32CS_SNAPPROCESS, 0 );
	pe32.dwSize = sizeof( PROCESSENTRY32 );

	Process32First( hProcessSnap, &pe32 );

	SendMessage(hList1, LB_RESETCONTENT, 0, 0);

	do
	{
		dwPriorityClass = 0;
		hProcess = OpenProcess( PROCESS_ALL_ACCESS, FALSE, pe32.th32ProcessID );
		dwPriorityClass = GetPriorityClass( hProcess );

		char text[1024];
		swprintf((wchar_t*)text, L"[%i] %s (%s)", pe32.th32ProcessID, pe32.szExeFile, GetPriorityString(dwPriorityClass));
		int idx = SendMessage(hList1, LB_ADDSTRING, 0, (LPARAM)text);
		SendMessage(hList1, LB_SETITEMDATA, idx, (LPARAM)pe32.th32ProcessID);
	} while( Process32Next( hProcessSnap, &pe32 ) );

	CloseHandle( hProcessSnap );
}


LPWSTR GetPriorityString(int p) {
	if (p == IDLE_PRIORITY_CLASS)
		return L"Idle";
	if (p == BELOW_NORMAL_PRIORITY_CLASS)
		return L"Below Normal";
	if (p == NORMAL_PRIORITY_CLASS)
		return L"Normal";
	if (p == ABOVE_NORMAL_PRIORITY_CLASS)
		return L"Above Normal";
	if (p == HIGH_PRIORITY_CLASS)
		return L"High";
	if (p == REALTIME_PRIORITY_CLASS)
		return L"Realtime";
	return L"Unknown";
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;
	PAINTSTRUCT ps;
	HDC hdc;

	switch (message)
	{
	case WM_CONTEXTMENU:
		{
			if (wParam == (LPARAM)hList1) {
				HMENU hPopupMenu = CreatePopupMenu();
				InsertMenu(hPopupMenu, 0, MF_BYPOSITION | MF_STRING, 10, L"Realtime");
				InsertMenu(hPopupMenu, 0, MF_BYPOSITION | MF_STRING, 11, L"High");
				InsertMenu(hPopupMenu, 0, MF_BYPOSITION | MF_STRING, 12, L"Above Normal");
				InsertMenu(hPopupMenu, 0, MF_BYPOSITION | MF_STRING, 13, L"Normal");
				InsertMenu(hPopupMenu, 0, MF_BYPOSITION | MF_STRING, 14, L"Below Normal");
				InsertMenu(hPopupMenu, 0, MF_BYPOSITION | MF_STRING, 15, L"Idle");
				SetForegroundWindow(hWnd);
				POINT p;
				GetCursorPos(&p);
				TrackPopupMenu(hPopupMenu, TPM_BOTTOMALIGN | TPM_LEFTALIGN, p.x, p.y, 0, hWnd, NULL);
			}
		}
	case WM_COMMAND:
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);

		if (wmId >= 10 && wmId <= 15) {
			int index = SendMessage(hList1, LB_GETCURSEL, 0, 0);
			if (index == LB_ERR)
				break;
			int pid = SendMessage(hList1, LB_GETITEMDATA, index, 0);

			int priority = 0;
			switch(wmId) {
			case 10:
				priority = REALTIME_PRIORITY_CLASS;
				break;
			case 11:
				priority = HIGH_PRIORITY_CLASS;
				break;
			case 12:
				priority = ABOVE_NORMAL_PRIORITY_CLASS;
				break;
			case 13:
				priority = NORMAL_PRIORITY_CLASS;
			case 14:
				priority = BELOW_NORMAL_PRIORITY_CLASS;
				break;
			case 15:
				priority = IDLE_PRIORITY_CLASS;
				break;
			}

			HANDLE hProcess = OpenProcess( PROCESS_ALL_ACCESS, FALSE, pid);
			if (!SetPriorityClass(hProcess, priority))
				MessageBox(hWnd, L"Failed to set priority", L"", MB_ICONERROR);
			CloseHandle(hProcess);
			Reload();
		}

		switch (wmId)
		{
		case 1: {
			if (wmEvent == NM_RCLICK) {
				int index = SendMessage(hList1, LB_GETCURSEL, 0, 0);

			}
			if (wmEvent != 0) {
				int index = SendMessage(hList1, LB_GETCURSEL, 0, 0);
				if (index == LB_ERR)
					break;
				int pid = SendMessage(hList1, LB_GETITEMDATA, index, 0);

				SendMessage(hList2, LB_RESETCONTENT, 0, 0);
				HANDLE hModuleSnap = INVALID_HANDLE_VALUE;
				MODULEENTRY32 me32;

				hModuleSnap = CreateToolhelp32Snapshot( TH32CS_SNAPMODULE, pid);
				me32.dwSize = sizeof( MODULEENTRY32 );

				Module32First( hModuleSnap, &me32 );

				do
				{
					int idx = SendMessage(hList2, LB_ADDSTRING, 0, (LPARAM)me32.szModule);
					SendMessage(hList2, LB_SETITEMDATA, idx, (LPARAM)me32.szModule);


					_tprintf( TEXT("\n\n     MODULE NAME:     %s"),   me32.szModule );
					_tprintf( TEXT("\n     Executable     = %s"),     me32.szExePath );
					_tprintf( TEXT("\n     Process ID     = 0x%08X"),         me32.th32ProcessID );
					_tprintf( TEXT("\n     Ref count (g)  = 0x%04X"),     me32.GlblcntUsage );
					_tprintf( TEXT("\n     Ref count (p)  = 0x%04X"),     me32.ProccntUsage );
					_tprintf( TEXT("\n     Base address   = 0x%08X"), (DWORD) me32.modBaseAddr );
					_tprintf( TEXT("\n     Base size      = %d"),             me32.modBaseSize );

				} while( Module32Next( hModuleSnap, &me32 ) );

				CloseHandle( hModuleSnap );
			}
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
