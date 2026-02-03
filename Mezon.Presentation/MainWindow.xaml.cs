using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Mezon.Presentation
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closed += MainWindow_Closed;
            LoadMockData();
        }

        private void MainWindow_Closed(object sender, WindowEventArgs args)
        {
            // Khi cửa sổ chính đóng -> Giết toàn bộ ứng dụng
            // Nếu không có dòng này, WindowService vẫn giữ tham chiếu hoặc tiến trình vẫn chạy
            Microsoft.UI.Xaml.Application.Current.Exit();
        }

        // Các ObservableCollection giúp UI tự động cập nhật khi dữ liệu thay đổi
        public ObservableCollection<Server> Servers { get; set; } = new();
        public ObservableCollection<DirectMessage> DirectMessages { get; set; } = new();
        public ObservableCollection<ChatMessage> Messages { get; set; } = new();

        private void LoadMockData()
        {
            // 1. Dữ liệu Server - Bao gồm cả server có icon và server dùng chữ cái đầu
            Servers.Add(new Server { Name = "Trang chủ", IconUrl = "https://assets-global.website-files.com/6257adef93867e56f84d3092/636e0a6a49cf127bf92de1e2_icon_clyde_blurple_RGB.png" });
            Servers.Add(new Server { Name = "Gaming Hub", IconUrl = "https://images.unsplash.com/photo-1542751371-adc38448a05e?auto=format&fit=crop&w=100&q=80" });
            Servers.Add(new Server { Name = "Dev Team", IconUrl = "https://images.unsplash.com/photo-1555099962-4199c345e5dd?auto=format&fit=crop&w=100&q=80" });

            // FIX: Cung cấp IconUrl hợp lệ cho các server này để tránh lỗi "Parameter incorrect" khi binding Image.Source
            Servers.Add(new Server
            {
                Name = "Chill Zone",
                Initials = "CZ",
                HasNoIcon = Visibility.Visible,
                IconUrl = "https://ui-avatars.com/api/?name=CZ&background=6366f1&color=fff" // Ảnh nền màu tím nhạt
            });

            Servers.Add(new Server { Name = "Art Gallery", IconUrl = "https://images.unsplash.com/photo-1513364776144-60967b0f800f?auto=format&fit=crop&w=100&q=80" });

            // FIX: Cung cấp IconUrl hợp lệ
            Servers.Add(new Server
            {
                Name = "Mezon Community",
                Initials = "MC",
                HasNoIcon = Visibility.Visible,
                IconUrl = "https://ui-avatars.com/api/?name=MC&background=22c55e&color=fff" // Ảnh nền màu xanh lá
            });

            // 2. Dữ liệu Tin nhắn trực tiếp (Bạn bè) & Trạng thái
            // Màu trạng thái: Xanh (#23A559), Vàng (#FAA61A), Đỏ (#F23F42), Xám (#747F8D)
            DirectMessages.Add(new DirectMessage
            {
                Name = "Phạm Giang",
                AvatarUrl = "https://ui-avatars.com/api/?name=Pham+Giang&background=0D8ABC&color=fff",
                StatusColor = "#23A559",
                Activity = "Đang chơi League of Legends"
            });

            DirectMessages.Add(new DirectMessage
            {
                Name = "Ru",
                AvatarUrl = "https://ui-avatars.com/api/?name=Ru&background=random",
                StatusColor = "#FAA61A",
                Activity = "Chờ"
            });

            DirectMessages.Add(new DirectMessage
            {
                Name = "Sảnh hightka FA",
                AvatarUrl = "https://ui-avatars.com/api/?name=Sanh&background=random",
                StatusColor = "#23A559",
                Activity = "Đang nghe Spotify"
            });

            DirectMessages.Add(new DirectMessage
            {
                Name = "Otis1905",
                AvatarUrl = "https://ui-avatars.com/api/?name=Otis&background=random",
                StatusColor = "#F23F42",
                Activity = "Không làm phiền",
                HasActivity = Visibility.Visible
            });

            DirectMessages.Add(new DirectMessage
            {
                Name = "Híu",
                AvatarUrl = "https://ui-avatars.com/api/?name=Hiu&background=random",
                StatusColor = "#747F8D",
                Activity = "",
                HasActivity = Visibility.Collapsed
            });

            DirectMessages.Add(new DirectMessage
            {
                Name = "Ticket King",
                AvatarUrl = "https://ui-avatars.com/api/?name=Ticket+King&background=5865F2&color=fff",
                StatusColor = "#5865F2",
                Activity = "/help | ticketking.xyz",
                HasActivity = Visibility.Visible
            });

            // 3. Dữ liệu Tin nhắn chat
            Messages.Add(new ChatMessage
            {
                UserName = "Phạm Giang",
                AvatarUrl = "https://ui-avatars.com/api/?name=Pham+Giang&background=0D8ABC&color=fff",
                Content = "Ê tối nay làm vài ván không?",
                TimeStamp = "Hôm nay 19:30"
            });

            Messages.Add(new ChatMessage
            {
                UserName = "MezonUser",
                AvatarUrl = "https://ui-avatars.com/api/?name=Mezon+User",
                Content = "Oke luôn, tầm 8h nhé. Đợi cơm nước xong đã.",
                TimeStamp = "Hôm nay 19:32"
            });

            Messages.Add(new ChatMessage
            {
                UserName = "Phạm Giang",
                AvatarUrl = "https://ui-avatars.com/api/?name=Pham+Giang&background=0D8ABC&color=fff",
                Content = "Nhớ rủ thêm thằng Ru nữa, nó đang ngồi chờ ở sảnh nãy giờ.",
                TimeStamp = "Hôm nay 19:33"
            });

            Messages.Add(new ChatMessage
            {
                UserName = "MezonUser",
                AvatarUrl = "https://ui-avatars.com/api/?name=Mezon+User",
                Content = "Rồi để tao pm nó.",
                TimeStamp = "Hôm nay 19:33"
            });

            Messages.Add(new ChatMessage
            {
                UserName = "Phạm Giang",
                AvatarUrl = "https://ui-avatars.com/api/?name=Pham+Giang&background=0D8ABC&color=fff",
                Content = "Mà nay có sự kiện mới đấy, vào check xem. Thấy bảo skin mới đẹp lắm.",
                TimeStamp = "Hôm nay 19:35"
            });

            Messages.Add(new ChatMessage
            {
                UserName = "MezonUser",
                AvatarUrl = "https://ui-avatars.com/api/?name=Mezon+User",
                Content = "Tiền đâu mà mua 🥲",
                TimeStamp = "Hôm nay 19:36"
            });
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic xử lý khi nhấn nút Home (ví dụ: chuyển đổi View sang danh sách bạn bè)
        }
    }

    // --- Data Models (Các lớp dữ liệu) ---

    public class Server
    {
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public string Initials { get; set; }
        // Visibility.Collapsed mặc định cho server có hình ảnh
        public Visibility HasNoIcon { get; set; } = Visibility.Collapsed;
    }

    public class DirectMessage
    {
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public string StatusColor { get; set; } // Mã màu Hex cho trạng thái (Online, Idle...)
        public string Activity { get; set; }
        public Visibility HasActivity { get; set; } = Visibility.Visible;
    }

    public class ChatMessage
    {
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
        public string Content { get; set; }
        public string TimeStamp { get; set; }
    }
}
