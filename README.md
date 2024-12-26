![GitHub repo size](https://img.shields.io/github/repo-size/cc-Mehdi/Files-Circulation-Management-System)

# سیستم مدیریت گردش پرونده
هدف از این پروژه، ایجاد سیستمی برای مدیریت و پیگیری گردش پرونده‌ها در یک اداره است. این سیستم به کاربران اجازه می‌دهد تا وضعیت فعلی پرونده‌های مرتبط با متقاضیان را مشاهده، جستجو، و در صورت نیاز به‌روزرسانی کنند. این پروژه طراحی شده است تا فرآیند مدیریت پرونده‌ها را ساده‌تر و دقیق‌تر کند و امکان نظارت بهتر بر انتقال و پردازش پرونده‌ها در بین بخش‌های مختلف اداره را فراهم آورد.

## جدول محتوا
* [نرم‌ افزار](#نرمافزار)
* [تکنولوژی‌ها](#تکنولوژیها)
* [پیش نیازها](#پیشنیازها)
* [راه‌اندازی](#راهاندازی)
* [نحوه استفاده](#نحوه-استفاده)
* [امکانات](#امکانات)


## نرم‎‌‎‌افزار
  در ادامه تصاویری از نرم افزار وجود دارد که ممکنه با ورژن نهایی کمی متفاوت باشد ولی میتوانید ذهنیت کمی درباره خروجی داشته باشید. 😉
  
  ![demo](https://github.com/user-attachments/assets/031bd799-71d2-45e4-b531-f5f4a784e2cb)

  

## تکنولوژی‌ها
* سی شارپ - CSharp
* ویندوز فرم (WinForms)
* دیتابیس (SQL Server)

## پیش‌نیازها
قبل از شروع، مطمئن شوید که پیش‌نیازهای زیر روی سیستم شما نصب هستند:
* نصب Visual Studio (نسخه 2019 یا 2022 توصیه می‌شود): **(این مورد برای توسعه نیاز است)**
* هنگام نصب Visual Studio، مطمئن شوید که گزینه‌ی .NET Desktop Development را انتخاب کرده‌اید. **(این مورد برای توسعه نیاز است)**
* نصب SQL Server:  **(این مورد برای خروجی ضروری است)**
* برای مدیریت پایگاه داده (نسخه‌های SQL Server Express یا کامل قابل استفاده هستند). **(این مورد برای خروجی ضروری است)**
* نصب SQL Server Management Studio (SSMS): **(این مورد برای خروجی ضروری است)**
* برای مدیریت و ایجاد پایگاه داده. **(این مورد برای خروجی ضروری است)**

## راه‌اندازی
1. برای اجرای این پروژه کافیست این پوشه را در کامپیوتر خود دانلود یا کلون کنید. `git clone https://github.com/cc-Mehdi/Files-Circulation-Management-System.git`
2. دیتابیس رو ایجاد کنید :
   2_1. فایل بکاپ را در SSMS خود اجرا کنید.
   2_2. همچنین میتوانید از طریق فایل های اسکریپتی که در فایل .sln قرار داره کوئری اجرا کنید و دیتابیس خود را ایجاد کنید. آدرس پوشه : `Files-Circulation-Management-System\src\db_class_office_project\Database Scripts`
4. برای اجرای مستقیم نرم افزار بدون VS پوشه نرم افزار مورد نظر را انتخاب کنید و به مسیر `نام پوشه > پوشه Debug > فایل.exe` را اجرا کنید **(برای نمایش خروجی)**
5. از برنامه لذت ببرید!
