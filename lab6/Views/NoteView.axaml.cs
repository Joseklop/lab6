using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace lab6.Views
{
    public partial class NoteView : UserControl
    {
        public NoteView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
