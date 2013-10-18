#include "stdafx.h"
#include "Lab6.h"

HINSTANCE hInst;				
TCHAR* szTitle = _T("Title");				
TCHAR* szText = _T("Scrolling text");				
TCHAR* szWindowClass = _T("Lab1");
HWND hButton1, hButton2;

LPWSTR letters[5];
UINT letterX[5];
INT letterY[5];
UINT letterSpeed[5];
HANDLE threads[5];

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
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_LAB6));
	wcex.hCursor		= LoadCursor(hInstance, MAKEINTRESOURCE(IDC_HAND));
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_LAB6);
	wcex.lpszClassName	= szWindowClass;
	wcex.hIconSm		= LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_LAB6));

	return RegisterClassEx(&wcex);
}



bool running = TRUE;


DWORD WINAPI ThreadProc(LPVOID param) {
	int idx = (int)param;
	while (1) {
		Sleep(10);
		letterY[idx] += letterSpeed[idx];
		if (letterY[idx] > 500)
			letterY[idx]= -30;
	}
}

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	HWND hWnd;

	hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, NULL, NULL, hInstance, NULL);

	if (!hWnd)
	{
		return FALSE;
	}

	MoveWindow(hWnd, 0, 0, 640, 480, FALSE);
	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);


	hButton1 = CreateWindowEx(WS_EX_APPWINDOW
		, L"BUTTON", L"Start"
		, WS_CHILD | WS_VISIBLE 
		, 0, 0, 128, 64
		, hWnd, (HMENU)1, hInstance, NULL);        

	hButton2 = CreateWindowEx(WS_EX_APPWINDOW
		, L"BUTTON", L"Stop"
		, WS_CHILD | WS_VISIBLE 
		, 500, 0, 128, 64
		, hWnd, (HMENU)2, hInstance, NULL); 


	letters[0] = L"A";
	letters[1] = L"B";
	letters[2] = L"C";
	letters[3] = L"D";
	letters[4] = L"E";
	letterSpeed[0] = 1;
	letterSpeed[1] = 2;
	letterSpeed[2] = 3;
	letterSpeed[3] = 4;
	letterSpeed[4] = 5;
	letterX[0] = 220;
	letterX[1] = 240;
	letterX[2] = 260;
	letterX[3] = 280;
	letterX[4] = 320;
	letterY[0] = 0;
	letterY[1] = 0;
	letterY[2] = 0;
	letterY[3] = 0;
	letterY[4] = 0;

	for (int i = 0; i < 5; i++)
		threads[i] = CreateThread(NULL, 2000, &ThreadProc, (LPVOID)i, 0, NULL);

	SetTimer(hWnd, 1, 3, NULL);

	return TRUE;
}

void DoStart(HWND hWnd) {
	if (!running) {
		for (int i = 0; i < 5; i++)
			ResumeThread(threads[i]);
	}
	running = TRUE;
}

void DoStop(HWND hWnd) {
	if (running) {
		for (int i = 0; i < 5; i++)
			SuspendThread(threads[i]);
	}
	running = FALSE;
}

void DoTimer(HWND hWnd) {
	InvalidateRect(hWnd, NULL, TRUE);
}

void DoPaint(HDC hdc) {
	RECT rect;
	SetTextColor(hdc, 0x00000000);
	SetBkMode(hdc, TRANSPARENT);

	for (int i = 0; i < 5; i++) {
		rect.left = letterX[i];
		rect.top = letterY[i];
		rect.right = letterX[i] + 100;
		rect.bottom = letterY[i] + 100;
		DrawText(hdc, letters[i], -1, &rect, DT_SINGLELINE | DT_NOCLIP  ) ;
	}
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
		case 1:
			DoStart(hWnd);
			break;
		case 2:
			DoStop(hWnd);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		break;
	case WM_TIMER:
		DoTimer(hWnd);
		break;
	case WM_PAINT:
		hdc = BeginPaint(hWnd, &ps);
		DoPaint(hdc);
		EndPaint(hWnd, &ps);
		break;
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}
