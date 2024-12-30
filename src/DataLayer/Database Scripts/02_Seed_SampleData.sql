-- Insert additional sample records into FileStatus
INSERT INTO FileStatus (Title) VALUES
('مدیر ارشد'),
('تحلیل‌گر داده'),
('مسئول پیگیری'),
('مشاور حقوقی'),
('برنامه‌ریز پروژه');
GO

-- Insert sample records into Users
INSERT INTO Users (Fullname, Username, HashPassword, Phone, Email, PictureAddress) VALUES
('علی محمدی', 'alim', 'hashedpass123', '09121234567', 'ali.m@example.com', 'images/ali.jpg'),
('سارا حسینی', 'sarah', 'hashedpass456', '09129876543', 'sara.h@example.com', 'images/sara.jpg'),
('حمید رضایی', 'hamidr', 'hashedpass789', '09121112222', 'hamid.r@example.com', 'images/hamid.jpg'),
('نازنین احمدی', 'nazanin', 'hashedpass321', '09125554433', 'nazanin.a@example.com', 'images/nazanin.jpg'),
('admin', 'admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', '', '', ''), -- admin user
('مجید تقوی', 'majidt', 'hashedpass654', '09126667788', 'majid.t@example.com', 'images/majid.jpg');
GO

-- Insert additional sample records into Files
INSERT INTO Files (FileStatus_Id, User_Id, CaseId, FullName, SubscriptionCode) VALUES
(1, 1, 2230, 'امید فرهادی', '555666'),
(3, 2, 2231, 'الهام نوروزی', '777888'),
(2, 3, 2232, 'حسن جعفری', '999000'),
(4, 4, 2233, 'فاطمه مرادی', '112233'),
(5, 5, 2234, 'محمد کریمی', '445566'),
(1, 1, 2235, 'سینا محمودی', '667788'),
(3, 2, 2236, 'زینب حیدری', '334455');
GO
