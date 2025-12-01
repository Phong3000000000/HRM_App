using System.Text.Json.Serialization; // 👉 Nhớ thêm dòng này

namespace HRMApp.Model
{
    public class ApiResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        // Backend trả về "statusCode", ta cần hứng nó vào đây
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        // Tự động tính toán: Nếu StatusCode trong khoảng 200-299 là Thành công
        public bool Success => StatusCode >= 200 && StatusCode < 300;

        // Thêm IsSuccess để tương thích với các đoạn code ViewModel đang gọi .IsSuccess
        public bool IsSuccess => Success;
    }

    public class ApiResponse<T>
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }

        // Tự động tính toán Success
        public bool Success => StatusCode >= 200 && StatusCode < 300;

        // Thêm IsSuccess để tương thích
        public bool IsSuccess => Success;
    }
}