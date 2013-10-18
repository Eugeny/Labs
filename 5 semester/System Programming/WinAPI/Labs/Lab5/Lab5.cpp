#include "stdafx.h"
#include "Lab5.h"

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;								// current instance
TCHAR szTitle[MAX_LOADSTRING];					// The title bar text
TCHAR szWindowClass[MAX_LOADSTRING];			// the main window class name

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
	LoadString(hInstance, IDC_LAB5, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_LAB5));

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
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_LAB5));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_LAB5);
	wcex.lpszClassName	= szWindowClass;
	wcex.hIconSm		= LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassEx(&wcex);
}


UINT messageId;

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	HWND hWnd;

	hInst = hInstance; 

	hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, NULL, NULL, hInstance, NULL);


	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	messageId = RegisterWindowMessage(L"Draw");

	return TRUE;
}


HBRUSH bRed = CreateSolidBrush(RGB(255,0,0));
HBRUSH bGreen = CreateSolidBrush(RGB(0,255,0));
HBRUSH bBlue = CreateSolidBrush(RGB(0,0,255));

UINT shapeCount = 0;
UINT shapeX[1024];
UINT shapeY[1024];
UINT shapeColor[1024];
UINT shapeType[1024];

UINT settings;
void DoPaint(HDC hdc) {
	for (int i = 0; i < shapeCount; i++) {
		UINT color = shapeColor[i];
		UINT shape = shapeType[i];
		UINT mouseX = shapeX[i];
		UINT mouseY = shapeY[i];

		SelectObject(hdc, (color == 0) ? bRed : ((color == 1) ? bGreen : bBlue));

		if (shape == 0) {
			POINT points[4];
			points[0].x = mouseX;
			points[0].y = mouseY - 10;
			points[1].x = mouseX + 10;
			points[1].y = mouseY;
			points[2].x = mouseX;
			points[2].y = mouseY + 10;
			points[3].x = mouseX - 10;
			points[3].y = mouseY;
			Polygon(hdc, points, 4);
		}
		if (shape == 1) {
			Rectangle(hdc, mouseX-10, mouseY-10, mouseX+10, mouseY+10);
		}
		if (shape == 2) {
			POINT points[10];
			points[0].x = mouseX;
			points[0].y = mouseY - 10;

			points[1].x = mouseX + 3;
			points[1].y = mouseY - 3;

			points[2].x = mouseX + 10;
			points[2].y = mouseY - 3;

			points[3].x = mouseX + 3;
			points[3].y = mouseY;

			points[4].x = mouseX + 5;
			points[4].y = mouseY + 10;

			points[5].x = mouseX;
			points[5].y = mouseY + 3;

			points[6].x = mouseX - 5;
			points[6].y = mouseY + 10;

			points[7].x = mouseX - 3;
			points[7].y = mouseY;

			points[8].x = mouseX - 10;
			points[8].y = mouseY - 3;

			points[9].x = mouseX - 3;
			points[9].y = mouseY - 3;

			Polygon(hdc, points, 10);
		}
		if (shape == 3) {
			Ellipse(hdc, mouseX - 10, mouseY - 10, mouseX+10, mouseY+10);
		}
	}
}


LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;
	PAINTSTRUCT ps;
	HDC hdc;

	if (message == messageId) {
		settings = wParam;
	}

	switch (message)
	{
	case WM_PAINT:
		hdc = BeginPaint(hWnd, &ps);
		DoPaint(hdc);
		EndPaint(hWnd, &ps);
		break;
	case WM_LBUTTONDOWN:  
		{
			if (settings & 16) {
				shapeX[shapeCount] = GET_X_LPARAM(lParam); 
				shapeY[shapeCount] = GET_Y_LPARAM(lParam); 
				shapeType[shapeCount] = (settings >> 2) & 3; 
				shapeColor[shapeCount] = settings & 3; 
				shapeCount ++;
			}
			InvalidateRect(hWnd, NULL, TRUE);
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
