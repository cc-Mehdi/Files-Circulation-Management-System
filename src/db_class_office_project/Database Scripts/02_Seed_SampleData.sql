-- Insert sample records into FileStatus
INSERT INTO FileStatus (Title) VALUES
('ریاست'),
('کارشناس گشت و بازرسی'),
('کارشناس منابع آب'),
('بایگانی'),
('کمیسیون');
GO

-- Insert sample records into Files
INSERT INTO Files (FileStatus_Id, CaseId, FullName, SubscriptionCode) VALUES
(1, 2225, 'رضا بختیار', '123456'), -- پرونده در ریاست
(4, 2226, 'علی رضایی', '654321'), -- پرونده در بایگانی
(2, 2227, 'مریم احمدی', '111222'), -- پرونده در گشت و بازرسی
(5, 2228, 'حسین معینی', '333444'); -- پرونده در کمیسیون
GO