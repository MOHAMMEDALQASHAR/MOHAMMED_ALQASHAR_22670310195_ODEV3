namespace Gorusel_odevi2;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();


        MainPage = new AppShell();

        // تعيين صفحة kaydol كأول صفحة يتم عرضها عند التشغيل
        Shell.Current.GoToAsync("//Kaydol");

    }
}
