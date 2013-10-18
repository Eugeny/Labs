#include "stdafx.h"
#include "Lab1.h"

HINSTANCE hInst;				
TCHAR* szTitle = _T("Title");				
TCHAR* szText = _T("Scrolling text");				
TCHAR* szWindowClass = _T("Lab1");

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
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_LAB1));
	wcex.hCursor		= LoadCursor(hInstance, MAKEINTRESOURCE(IDC_POINTER));
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_LAB1);
	wcex.lpszClassName	= szWindowClass;
	wcex.hIconSm		= LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_LAB1));

	return RegisterClassEx(&wcex);
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

	MoveWindow(hWnd, 0, 0, 320, 200,FALSE);
	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	return TRUE;
}


int textPosition = 0;
bool textRunning = FALSE;

void DoStart(HWND hWnd) {
	if (!textRunning)
		SetTimer(hWnd, 1, 3, NULL);
	textRunning = TRUE;
}

void DoStop(HWND hWnd) {
	if (textRunning)
		KillTimer(hWnd, 1);
	textRunning = FALSE;
}

void DoTimer(HWND hWnd) {
	RECT rect;
	GetWindowRect(hWnd, &rect);
	textPosition += 1;
	if (textPosition > rect.right - rect.left)
		textPosition = -100;
	InvalidateRect(hWnd, NULL, TRUE);
}

void DoPaint(HDC hdc) {
	RECT rect;
	SetTextColor(hdc, 0x00000000);
	SetBkMode(hdc,TRANSPARENT);
	rect.left = textPosition;
	rect.top = 10;
	rect.right = textPosition + 200;
	rect.bottom = 100;
	DrawText( hdc, szText, -1, &rect, DT_SINGLELINE | DT_NOCLIP  ) ;
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
		case IDM_START:
			DoStart(hWnd);
			break;
		case IDM_STOP:
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
