# CapstoneProject_SP24

## Mô Tả
- Sửa lỗi trả về response 405 mỗi khi cập nhật cây và xóa cây
- Sửa lại cấu trúc qua dùng CQRS và mediator pattern

## Vấn đề
### I. Dùng CQRS thay cho việc dùng TreeService
Thay đổi cấu trúc file từ
```
_Application
    |___Tree
          |___ITreeRepository.cs
          |___ITreeRepository.cs
          |___ITreeRepository.cs
```
thành
```
_Application
    |___Tree
          |___ Commands
          |       |___ Add
          |       |     |___ AddTreeCommand.cs
          |       |     |___ AddTreeHandler.cs
          |       |     |___ AddTreeValidator.cs
          |       |___ Delete
          |       |     |___ DeleteTreeCommand.cs
          |       |     |___ DeleteTreeHandler.cs
          |       |     |___ DeleteTreeValidator.cs
          |       |___ Update
          |             |___ UpdateTreeCommand.cs
          |             |___ UpdateTreeHandler.cs
          |             |___ UpdateTreeValidator.cs
          |___ Common
          |       |___ TreeResult.cs
          |___ Queries
                  |___ GetById
                  |     |___ GetByIdTreeHandler.cs
                  |     |___ GetByIdTreeQuery.cs
                  |     |___ GetByIdTreeValidator.cs
                  |___ Delete
                        |___ ListTreeHandler.cs
                        |___ ListTreeQuery.cs

```
- Giải thích:
    - Commands: là lưu trữ các chức năng ghi vào db
    - Common là chứa các file chung cho cả queries và command đều dùng
    - Queries là dành cho việc đọc dữ liệu từ db
    - <name_feature> với hậu tố query hay là command là các request, còn handler là để xử lý logic và Validator là để tạo là các ràng buộc cho các thuộc tính bên trong request. Ví dụ như:
```
    public class AddTreeValidator : AbstractValidator<AddTreeCommand>
    {
        public AddTreeValidator()
        {
            RuleFor(x => x.id).NotEmpty(); // id không rỗng
        }
    }
```

### II. Bug về việc delete trả về thông báo 405
#### Phần Delete
 Ở trong phần `TreeRepository.cs`, đã sửa lại hàm `DeleteTree` từ
```
        public void DeleteTree(int id)
        {
            _treeDbContext.Remove(id);
            _treeDbContext.SaveChanges();
        }
```
thành 
```
        public void DeleteTree(int id)
        {
            var tree = _treeDbContext.Trees.FirstOrDefault(t => t.Id == id);
            _treeDbContext.Trees.Remove(tree);
            _treeDbContext.SaveChanges();
        }
```
và sửa cả controller của cây lấy thêm id của cây
=> Nguyên nhân là do `dbContext.Remove(Object entity)` thì nó yêu cầu 1 `entity` nhưng do truyền vào đó là kiểu `int` nên sẽ báo không có entity nào là `int` cả
