// Lab5.Toolbar.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "Lab5.Toolbar.h"

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;								// current instance
TCHAR szTitle[MAX_LOADSTRING];					// The title bar text
TCHAR szWindowClass[MAX_LOADSTRING];			// the main window class name

HWND hRdColors[3];
HWND hRdShape[4];
HWND hChkDraw;


// Forward declarations of functions included in this code module:
ATOM				MyRegisterClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);

int APIENTRY _tWinMain(_In_ HINSTANCE hInstance,
					   _In_opt_ HINSTANCE hPrevInstance,
					   _In_ LPTSTR    lpCmdLine,
					   _In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	// TODO: Place code here.
	MSG msg;
	HACCEL hAccelTable;

	// Initialize global strings
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_LAB5TOOLBAR, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_LAB5TOOLBAR));

	// Main message loop:
	while (GetMessage(&msg, NULL, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}

	return (int) msg.wParam;
}



//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style			= CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc	= WndProc;
	wcex.cbClsExtra		= 0;
	wcex.cbWndExtra		= 0;
	wcex.hInstance		= hInstance;
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_LAB5TOOLBAR));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_LAB5TOOLBAR);
	wcex.lpszClassName	= szWindowClass;
	wcex.hIconSm		= LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassEx(&wcex);
}

UINT  messageId;
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	HWND hWnd;

	hInst = hInstance; // Store instance handle in our global variable

	hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, NULL, NULL, hInstance, NULL);


	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);


	hRdColors[0] = CreateWindowEx(WS_EX_APPWINDOW
		, L"BUTTON", L"Red"
		, WS_CHILD | WS_VISIBLE | BS_AUTORADIOBUTTON | WS_GROUP
		, 0, 0, 200, 50
		, hWnd, (HMENU)1, hInstance, NULL);        

	hRdColors[1] = CreateWindowEx(WS_EX_APPWINDOW
		, L"BUTTON", L"Green"
		, WS_CHILD | WS_VISIBLE | BS_AUTORADIOBUTTON
		, 0, 50, 200, 50
		, hWnd, (HMENU)1, hInstance, NULL);        

	hRdColors[2] = CreateWindowEx(WS_EX_APPWINDOW
		, L"BUTTON", L"Blue"
		, WS_CHILD | WS_VISIBLE | BS_AUTORADIOBUTTON
		, 0, 100, 200, 50
		, hWnd, (HMENU)1, hInstance, NULL);        


	hRdShape[0] = CreateWindowEx(WS_EX_APPWINDOW
		, L"BUTTON", L"Spade"
		, WS_CHILD | WS_VISIBLE | BS_AUTORADIOBUTTON | WS_GROUP
		, 0, 150, 200, 50
		, hWnd, (HMENU)1, hInstance, NULL);        
	hRdShape[1] = CreateWindowEx(WS_EX_APPWINDOW
		, L"BUTTON", L"Square"
		, WS_CHILD | WS_VISIBLE | BS_AUTORADIOBUTTON 
		, 0, 200, 200, 50
		, hWnd, (HMENU)1, hInstance, NULL);        
	hRdShape[2] = CreateWindowEx(WS_EX_APPWINDOW
		, L"BUTTON", L"Star"
		, WS_CHILD | WS_VISIBLE | BS_AUTORADIOBUTTON 
		, 0, 250, 200, 50
		, hWnd, (HMENU)1, hInstance, NULL);        
	hRdShape[3] = CreateWindowEx(WS_EX_APPWINDOW
		, L"BUTTON", L"Circle"
		, WS_CHILD | WS_VISIBLE | BS_AUTORADIOBUTTON 
		, 0, 300, 200, 50
		, hWnd, (HMENU)1, hInstance, NULL);        

	hChkDraw = CreateWindowEx(WS_EX_APPWINDOW
		, L"BUTTON", L"Draw"
		, WS_CHILD | WS_VISIBLE | BS_AUTOCHECKBOX
		, 0, 350, 200, 50
		, hWnd, (HMENU)1, hInstance, NULL);        

	messageId = RegisterWindowMessage(L"Draw");

	MoveWindow(hWnd, 20, 20, 200, 600, FALSE);
	SendMessage(hRdColors[0], BM_SETCHECK, BST_CHECKED, 0);
	SendMessage(hRdShape[0], BM_SETCHECK, BST_CHECKED, 0);
	SendMessage(hChkDraw, BM_SETCHECK, BST_CHECKED, 0);

	PostMessage(hWnd, WM_COMMAND, 0, 0);
	return TRUE;
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;
	PAINTSTRUCT ps;
	HDC hdc;

	switch (message)
	{
	case WM_COMMAND: {
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);

		// Send the settings
		UINT color = 0;
		if (SendMessage(hRdColors[0], BM_GETCHECK, 0, 0) == BST_CHECKED) color = 0;
		if (SendMessage(hRdColors[1], BM_GETCHECK, 0, 0) == BST_CHECKED) color = 1;
		if (SendMessage(hRdColors[2], BM_GETCHECK, 0, 0) == BST_CHECKED) color = 2;

		UINT shape = 0;
		if (SendMessage(hRdShape[0], BM_GETCHECK, 0, 0) == BST_CHECKED) shape = 0;
		if (SendMessage(hRdShape[1], BM_GETCHECK, 0, 0) == BST_CHECKED) shape = 1;
		if (SendMessage(hRdShape[2], BM_GETCHECK, 0, 0) == BST_CHECKED) shape = 2;
		if (SendMessage(hRdShape[3], BM_GETCHECK, 0, 0) == BST_CHECKED) shape = 3;

		UINT draw = SendMessage(hChkDraw, BM_GETCHECK, 0, 0) == BST_CHECKED ? 1 : 0;

		UINT settings = shape * 4 + color + 16 * draw;
		PostMessage(HWND_BROADCAST, messageId, settings, 0);

		// Parse the menu selections:
		switch (wmId)
		{
		case IDM_EXIT:
			DestroyWindow(hWnd);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		break;
					 }
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}