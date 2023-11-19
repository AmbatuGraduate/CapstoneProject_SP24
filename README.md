# CapstoneProject_SP24

## Description
Đây là dự án đơn giản với crud, authen và authorize

## Sử dụng application architecture và design pattern
- [Clean architecture](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture)
- [QRS](https://topdev.vn/blog/cqrs-pattern-la-gi-vi-du-de-hieu-ve-cqrs-pattern/) và [Mediator](https://refactoring.guru/design-patterns/mediator)
## Cấu trúc bài
![image](https://github.com/quanSadie/CapstoneProject_SP24/assets/83583888/7f6155da-1f01-4264-827d-f6ff4cda6330)

* API và Contracts là 1 presentaion layer, cầu nối đến với người dùng thường chứa các controllers
  * Contracts chứa các model request của api như LoginRequest, RegisterRequest...   
* Infrastructure là 1 Infrastructure layer, có chức năng là triển khai truy cập dữ liệu từ DB
* Domain và Application là 1 Core layer, giữ các business model bao gồm các entity, serivce, interface
  

