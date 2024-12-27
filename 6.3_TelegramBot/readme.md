## Hướng dẫn sử dụng

### 1. Lấy Bot Token
- Mở ứng dụng Telegram và tìm kiếm BotFather (bot chính thức của Telegram để quản lý bot).
- Bắt đầu chat với BotFather và gửi lệnh: 
`/newbot`

Thực hiện theo hướng dẫn:
- Đặt tên cho bot của bạn (ví dụ: MyTestBot).
- Chọn một tên người dùng duy nhất cho bot (ví dụ: MyTestBot_bot).
- Sau khi tạo thành công, BotFather sẽ cung cấp cho bạn một token (một chuỗi ký tự bao gồm chữ, số và ký tự đặc biệt). Đây chính là BotToken của bạn.

Ví dụ về token:

```
123456789:ABCdefGHIjklMNOpqrSTUvwXYZ
```


### 2. Lấy Chat ID
#### Nếu gửi tin nhắn đến chính bạn:

- Mở chat với bot mà bạn vừa tạo và gửi một tin nhắn bất kỳ.

- Truy cập URL sau trên trình duyệt, thay <BOT_TOKEN> bằng token bot của bạn:


`https://api.telegram.org/bot<BOT_TOKEN>/getUpdates`

- Tìm đối tượng chat trong phản hồi (response). Trường id trong đó chính là ChatId.

Ví dụ:

```
{
    "update_id": 123456789,
    "message": {
        "chat": {
            "id": 987654321,
            "first_name": "John",
            "type": "private"
        },
        "text": "Hello"
    }
}
```
Trong ví dụ trên, id (987654321) chính là ChatId của bạn.

#### Nếu gửi tin nhắn đến một nhóm (group):

- Thêm bot của bạn vào nhóm.
- Gửi một tin nhắn bất kỳ trong nhóm.
- Thực hiện lại các bước trên để lấy ChatId từ phản hồi getUpdates. Lưu ý, đối với nhóm, ChatId thường có dấu âm (ví dụ: -123456789).

#### Lưu ý quan trọng

Bảo mật BotToken, không chia sẻ với người khác vì ai có nó đều có thể điều khiển bot của bạn.
Nếu bot không phản hồi, đảm bảo rằng bạn đã gửi lệnh `/start` cho bot ít nhất một lần.