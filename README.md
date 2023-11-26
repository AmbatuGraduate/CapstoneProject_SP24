# CapstoneProject_SP24

## Description
Đây là dự án đơn giản với crud, authen và authorize

## Sử dụng công nghệ, application architecture và design pattern
- [Clean architecture](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture)
- [CQRS](https://viblo.asia/p/trien-khai-ung-dung-don-gian-su-dung-cqrs-pattern-voi-raw-sql-va-ddd-gGJ59oy9ZX2) và [Mediator](https://refactoring.guru/design-patterns/mediator)
- Authentication và authorization bằng JWT token
## Cấu trúc bài
![image](https://github.com/quanSadie/CapstoneProject_SP24/assets/83583888/7f6155da-1f01-4264-827d-f6ff4cda6330)
* API và Contracts là 1 presentaion layer, cầu nối đến với người dùng thường chứa các controllers
  * Contracts chứa các model request của api như LoginRequest, RegisterRequest...   
* Infrastructure là 1 Infrastructure layer, có chức năng là triển khai truy cập dữ liệu từ DB
* Domain và Application là 1 Core layer, giữ các business model bao gồm các entity, serivce, interface

## Notice
### Bug về việc in ra thông tin lỗi
1. Vẫn có bug trong việc xuất ra thông tin lỗi khi check validation, nếu như có 1 list lỗi

 _Giả sử có 1 request register như sau_

```
{
  "name": "",
  "address": "",
  "phone": "09xxxxxxx",
  "password": "123123Aa!",
  "role": "Admin",
  "image": "string"
}
```
_Thì lỗi sẽ trả về như mong đợi sẽ như này_
 ```
  {
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-ec363af71c2dc4b6f5a5c9be1695ed6e-2e60d14f7e79e3bc-00",
    "errors": {
        "Name": [
           "'Name' must not be empty."
        ],
         "Address": [
           "'Address' must not be empty."
        ]  
     }
   }
```
_Nhưng lỗi trả về lại như này_
 ```
  {
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "00-ec363af71c2dc4b6f5a5c9be1695ed6e-2e60d14f7e79e3bc-00",
  "errors": {}
}
```
=> Nên sẽ khó phát hiện được mình bị lỗi gì.
* Nên từ dòng 19 đến 23 ở [đây](https://github.com/quanSadie/CapstoneProject_SP24/blob/test/crud_user/Webapp/Quan%20ly%20moi%20truong_Web/API/Controllers/ApiController.cs) đã được comment lại và sẽ chỉ hiện được 1 lỗi dưới dạng như sau khi có 1 list lỗi
  ```
  {
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "'Name' must not be empty.",
    "status": 400,
    "traceId": "00-e15424452da2ca2fe50bf4b0084b7439-f3ded61dd9a2d2ba-00"
  }
  ```
