#include "stdafx.h"
#include "Lab3.h"

HINSTANCE hInst;				
HWND hButton1, hButton2;
HBITMAP hbButtonNormal, hbButtonHover, hbButtonPressed;
bool imageActive = TRUE;

TCHAR* szTitle = _T("Lab 3");				
TCHAR* szWindowClass = _T("Lab3");

ATOM				RegisterWindowClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
void				DoPaint(HWND);

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
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_LAB3));
	wcex.hCursor		= LoadCursor(hInstance, MAKEINTRESOURCE(IDC_LAB3));
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_LAB3);
	wcex.lpszClassName	= szWindowClass;
	wcex.hIconSm		= LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_LAB3));

	return RegisterClassEx(&wcex);
}

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	HWND hWnd;

	hbButtonNormal = LoadBitmap(hInstance, MAKEINTRESOURCE(IDB_BUTTON_NORMAL));
	hbButtonHover = LoadBitmap(hInstance, MAKEINTRESOURCE(IDB_BUTTON_HOVER));
	hbButtonPressed = LoadBitmap(hInstance, MAKEINTRESOURCE(IDB_BUTTON_PRESSED));

	hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, NULL, NULL, hInstance, NULL);

	hButton1 = CreateWindowEx(WS_EX_APPWINDOW
		, L"BUTTON", L"TEST"
		, WS_CHILD | WS_VISIBLE | BS_OWNERDRAW
		, 0, 0, 128, 64
		, hWnd, (HMENU)1, hInstance, NULL);        

	hButton2 = CreateWindowEx(WS_EX_APPWINDOW
		, L"BUTTON", L"TEST"
		, WS_CHILD | WS_VISIBLE | BS_OWNERDRAW
		, 500, 0, 128, 64
		, hWnd, (HMENU)2, hInstance, NULL);        

	MoveWindow(hWnd, 0, 0, 640, 440, FALSE);
	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	DoPaint(hWnd);

	return TRUE;
}

void DrawButton(LPDRAWITEMSTRUCT dis) {
	HDC hDc = dis->hDC;
	BITMAP bitmap;

	HBITMAP hBitmap = hbButtonNormal;
	if (dis->itemState & ODS_HOTLIGHT)
		hBitmap = hbButtonHover;
	if (dis->itemState & ODS_SELECTED)
		hBitmap = hbButtonPressed;

	HDC hdcMem = CreateCompatibleDC(hDc);
	HBITMAP oldBitmap = (HBITMAP)SelectObject(hdcMem, hBitmap);
	GetObject(hBitmap, sizeof(bitmap), &bitmap);
	BitBlt(hDc, dis->rcItem.left, dis->rcItem.top, bitmap.bmWidth, bitmap.bmHeight, hdcMem, 0, 0, SRCCOPY);
	SelectObject(hdcMem, oldBitmap);

	LPWSTR text = L">";
	if (dis->hwndItem == hButton2)
		text = L"||";

	RECT rect;
	SetTextColor(hDc, 0x00000000);
	SetBkMode(hDc, TRANSPARENT);
	rect.left = 10;
	rect.top = 10;
	rect.right = 200;
	rect.bottom = 100;
	DrawText(hDc, text, -1, &rect, DT_SINGLELINE | DT_NOCLIP);
	DeleteDC(hdcMem);
}

HGDIOBJ penRed = CreatePen(NULL, 1, RGB(255, 0, 0));
HGDIOBJ penGray = CreatePen(NULL, 1, RGB(128, 128, 128));
HGDIOBJ penBrown = CreatePen(NULL, 1, RGB(128, 128, 0));
HGDIOBJ penBlack = CreatePen(NULL, 1, RGB(0, 0, 0));
HGDIOBJ penGreen2 = CreatePen(NULL, 1, RGB(0, 128, 0));

HGDIOBJ brushBlack = CreateSolidBrush(RGB(0, 0, 0));
HGDIOBJ brushGray = CreateSolidBrush(RGB(128, 128, 128));
HGDIOBJ brushGreen = CreateSolidBrush(RGB(128, 255, 128));
HGDIOBJ brushWhite = CreateSolidBrush(RGB(255, 255, 255));

void DoPaint(HWND hWnd) {
	HDC hDc = GetDC(hWnd);

	SelectObject(hDc, penGray); 
	SelectObject(hDc, brushWhite);
	Rectangle(hDc, -1, 90, 1000, 1000);

	if (imageActive) {
		int left = 100, top = 200;
		SelectObject(hDc, penGreen2); 
		SelectObject(hDc, brushGreen);
		Rectangle(hDc, left, top, left + 400, top + 100);
		Rectangle(hDc, left + 350, top + 70, left + 390, top + 90);
		Rectangle(hDc, left + 100, top + 70, left + 140, top + 90);
		Rectangle(hDc, left + 150, top + 70, left + 190, top + 90);

		SelectObject(hDc, penGray); 
		MoveToEx(hDc, left, top + 60, NULL);
		LineTo(hDc, left + 400, top + 60);
		MoveToEx(hDc, left, top + 62, NULL);
		LineTo(hDc, left + 400, top + 62);

		SelectObject(hDc, penBlack); 
		SelectObject(hDc, brushGray);
		Ellipse(hDc, left + 30, top + 80, left + 70, top + 120);
		Ellipse(hDc, left + 230, top + 80, left + 270, top + 120);
		Ellipse(hDc, left + 290, top + 80, left + 330, top + 120);

		SelectObject(hDc, penBrown);
		Arc(hDc, 
			left + 200, top - 50, 
			left + 400, top + 50,
			left + 300, top - 50,
			left + 200, top);
		Arc(hDc, 
			left + 210, top - 50, 
			left + 410, top + 50,
			left + 310, top - 50,
			left + 210, top);

		SelectObject(hDc, penBlack); 
		SelectObject(hDc, brushGray);

		POINT points[4];
		points[0].x = left + 20;
		points[0].y = top + 10;
		points[1].x = left + 60;
		points[1].y = top + 10;
		points[2].x = left + 60;
		points[2].y = top + 50;
		points[3].x = left + 10;
		points[3].y = top + 50;

		for (int i = 0; i < 6; i++) {
			Polygon(hDc, points, 4);
			for (int j = 0; j < 4; j++)
				points[j].x += 60;
		}
	}
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;
	PAINTSTRUCT ps;
	HDC hdc;

	switch (message)
	{
	case WM_DRAWITEM: {
		int itemId = wParam;
		LPDRAWITEMSTRUCT dis = (LPDRAWITEMSTRUCT)lParam;
		DrawButton(dis);
		break;
					  }
	case WM_COMMAND:
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		switch (wmId)
		{
		case 1: 
			imageActive = TRUE;
			DoPaint(hWnd);
			break;
		case 2: 
			imageActive = FALSE;
			DoPaint(hWnd);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		break;
	case WM_MOVE:
		DoPaint(hWnd);
		break;
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}
