using System.Windows;
using ManageMySim.Classes;

namespace ManageMySim
{

    public partial class MainWindow : Window
    {
        // First iteration on MainWindow before Login Window
        public MainWindow()
        {
            // Hide the window
            Hide();

            Utilities.startProgram();

        }

        // Second iteration, after login window
        public MainWindow(bool logged, string username)
        {
            InitializeComponent();

            // Set the position at the center of the screen
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = Width;
            double windowHeight = Height;
            Left = (screenWidth / 2) - (windowWidth / 2);
            Top = (screenHeight / 2) - (windowHeight / 2);

            // Set the label of the username with the user's username
            labelUser.Content = username;

            // Show the main window
            Show();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
