using System.Windows;

namespace GACore.DemoApp.Service;

public static class DialogService
{
	public static Window CreateFooWizardWindow()
	{
		FooWizardWindow window = new();
		return window;
	}
}
