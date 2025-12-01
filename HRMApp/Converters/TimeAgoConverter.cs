using System.Globalization;

namespace HRMApp.Converters
{
    public class TimeAgoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                // Tính khoảng thời gian chênh lệch
                var timeSpan = DateTime.Now - dateTime;

                // Nếu thời gian < 0 (do lệch múi giờ server), coi như vừa xong
                if (timeSpan.TotalSeconds < 0)
                    return "Vừa xong";

                // Dưới 1 phút
                if (timeSpan.TotalSeconds < 60)
                    return "Vừa xong";

                // Dưới 1 giờ
                if (timeSpan.TotalMinutes < 60)
                    return $"{(int)timeSpan.TotalMinutes} phút trước";

                // Dưới 24 giờ
                if (timeSpan.TotalHours < 24)
                    return $"{(int)timeSpan.TotalHours} giờ trước";

                // Dưới 7 ngày (Tuỳ chọn: hiện số ngày)
                if (timeSpan.TotalDays < 7)
                    return $"{(int)timeSpan.TotalDays} ngày trước";

                // Lâu hơn 1 tuần -> Hiện đầy đủ ngày tháng
                return dateTime.ToString("dd/MM/yyyy HH:mm");
            }

            return value; // Trả về giá trị gốc nếu không phải DateTime
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}