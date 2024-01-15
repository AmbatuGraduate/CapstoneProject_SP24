# CapstoneProject_SP24

## Mô Tả
- Sửa lại toàn bộ Domain dựa trên DB mà team đã chốt (tạm thời)
- Các chức năng CRUD cơ bản của cây, người dùng sẽ không thể dùng được do vẫn chưa sửa theo domain mới

## Giải thích về cấu trúc
```

|___Domain
|   |___Common
|   |    |___Errors
|   |    |___Models
|   |___Enitites
|___Application
|        |___Common
|        |   |___Behaviors
|        |   |___Interfaces
|        |   |   |___Authentication
|        |   |   |___Persistence
|        |   |   |___Service
|        |___Tree
|            |___ Commands
|            |       |___ Add
|            |       |     |___ AddTreeCommand.cs
|            |       |     |___ AddTreeHandler.cs
|            |       |     |___ AddTreeValidator.cs
|            |       |___ Delete
|            |       |     |___ DeleteTreeCommand.cs
|            |       |     |___ DeleteTreeHandler.cs
|            |       |     |___ DeleteTreeValidator.cs
|            |       |___ Update
|            |             |___ UpdateTreeCommand.cs
|            |             |___ UpdateTreeHandler.cs
|            |             |___ UpdateTreeValidator.cs
|            |___ Common
|            |       |___ TreeResult.cs
|            |___ Queries
|                    |___ GetById
|                    |     |___ GetByIdTreeHandler.cs
|                    |     |___ GetByIdTreeQuery.cs
|                    |     |___ GetByIdTreeValidator.cs
|                    |___ Delete
|                            |___ ListTreeHandler.cs
|                            |___ ListTreeQuery.cs
|
|
...
|
|
|___Infrastructure
|             |__Persistence
|             |       |___Configuration
|             |       |___Repositories
|             |___Services
|
|___API
|   |___Common
|   |      |___Errors
|   |      |___Http
|   |___Controllers
|   |___Mapping
|   |___DependencyInjection.cs
|
|
|___Contract
```
- Giải thích:
    + Domain: nơi sẽ chứa các entity của web và ngoài ra Domain không được `add reference` tới các class library bên ngoài, trong domain sẽ có các folder:
        + Common là nơi sẽ chưa các obj chung như là các class thông báo lỗi,...
            + Errors sẽ là nơi chứa các class báo lỗi và nhưng class đó sẽ dùng partial class, ví dụ class như sau
            ```
                public static partial class Errors
                {
                    public static class AddTree
                    {
                        public static Error AddTreeFail = Error.Failure(
                            code: "add.AddTree", description: "Add Fail.");
                    }
                }
            ```
            + Models thì sẽ là nơi chứa các class phục vụ cho việc dùng aggregate root (nhưng có thể sẽ xóa vì ko dùng)
        + Entities là nơi chứa các entity của web như cây, người dùng, role,...

    + Application là nơi sẽ thực hiện các bussiness logic
        + Interface.Authentiation là folder chứa các class interface dành cho authentication như là tạo Jwt token
        + Interface.Persistence là nơi chứa các interface repo cho Infrastructure
        + Khi muốn làm các func crud thì chỉ cần tạo 1 folder với tên là entity mà mình muốn là func crud và trong đó sẽ tạo thêm các folder là commands, common, và queries.
            + ví dụ như muốn làm crud cây
                + folder common là nơi chứa các class chung của 2 folder commands và queries sẽ dùng như là kết quả trả về
                + folder Commands có mục đích là dành cho các func nào sẽ ghi vào DB
                + folder Queries sẽ là nơi dành cho func đọc dữ liệu từ DB
                => lý do là làm theo CQRS pattern
                + Ngoài ra khi tạo 1 func trong commmands hay queries thì phải tạo 1 folder với tên chức năng mà bạn muốn tạo, ví dụ như muốn làm add tree thì chỉ tạo folder tên `Add`
                    + Trong folder `Add` hay là các folder chức năng khác trong tương lai hầu như sẽ có 3 class chính là request (khi đặt tên cho class này phải đặt là <tên func> + command hay query tùy vào func), validator (là nơi chứa các rule cho trường như là không được rỗng hay là phải có cùng regex, ...) và cuối cùng là handler func đó (đặt tên là <tên func> + handler)

    + Infrastructure là chủ yếu là dành cho các công việc liên quan đến DB
        + Persistence.Configuration là nơi sẽ config các property của entity
        + Persistence.Repositories là repo của entity

    + Contract là DTO request hoặc response của bên API và khi muốn tạo 1 request hay response DTP thì chỉ cần tạo 1 folder, ví dụ như đang làm 1 controller cho cây và muốn tạo 1 DTO request và response thì tạo 1 folder `tree` để lưu trữ các request và response

    + API thì là nơi sẽ giao tiếp với FE
        + Common.Errors là có 1 class được lấy trên mạng và cái này để handle các lỗi sẽ xảy ra
        + Common.Http thì là nơi để chứa các tên mà dùng chung qua nhiều hay lặp lại thì sẽ vào đó tạo 1 đn nghĩa key, ví dụ như từ `errors` được dùng nhiều thì tạo 1 `const sting Errors = "errors"` dùng rồi nếu như mà có thay đổi tên của key thì chỉ cần vào đây sửa 1 lần là cho hết toàn bộ
        + Controllers thì là nơi chứa các controller api của web; ngoài ra, `ApiController.cs` chính là class cha cho các API khác nên khi tạo mới thì kế thừa class đó
        => lý do là class `ApiController.cs` là class sẽ handle lỗi + có hàm override việc trả về detail lỗi nên mới cần kế thừa
        + Mapping thì là dành cho việc config mapping

### III. Thông tin thêm
- CQRS [HERE](https://topdev.vn/blog/cqrs-pattern-la-gi-vi-du-de-hieu-ve-cqrs-pattern/#:~:text=CQRS%20l%C3%A0%20pattern%20chia%20vi%E1%BB%87c,%C4%91%E1%BB%99t%20tr%C3%AAn%20c%C3%B9ng%20m%E1%BB%99t%20domain.)
- Mediator [HERE](https://refactoring.guru/design-patterns/mediator)
- Mapster [Nên coi đoạn đầu để hiểu về cách dùng mapster](https://www.youtube.com/watch?v=vBs6naPD6RE)
- Clean Architecture [Cái này sẽ giải thích ngắn gọn chút về clean architecture](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture)

