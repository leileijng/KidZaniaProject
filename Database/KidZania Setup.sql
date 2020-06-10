drop schema kidzania;
create schema kidzania;
use kidzania;

CREATE TABLE `email` (
  `uid` varchar(50) NOT NULL,
  `email` varchar(255) NOT NULL,
  PRIMARY KEY (`uid`));
  
  CREATE TABLE `item_photo` (
  `itemphoto_id` varchar(999) NOT NULL,
  `photo` varchar(100) NOT NULL,
  `lineitem_id` varchar(10) NOT NULL,
  `assigned_printer_id` varchar(10) NOT NULL,
  `printing_status` varchar(10) NOT NULL,
  `updated_at` datetime NOT NULL,
  PRIMARY KEY (`itemphoto_id`));
  
  CREATE TABLE `lineitem` (
  `lineitem_id` varchar(10) NOT NULL,
  `order_id` varchar(10) NOT NULL,
  `product_id` varchar(50) NOT NULL,
  `photos` varchar(999) NOT NULL,
  `item_amount` decimal(5,2) NOT NULL,
  `status` varchar(10) NOT NULL,
  PRIMARY KEY (`lineitem_id`));
  
  CREATE TABLE `order` (
  `order_id` varchar(10) NOT NULL,
  `pid` varchar(50) NOT NULL,
  `total_amount` decimal(5,2) NOT NULL,
  `status` varchar(10) NOT NULL,
  PRIMARY KEY (`order_id`));
  
  CREATE TABLE `printer` (
  `printer_id` varchar(10) NOT NULL,
  `name` varchar(50) NOT NULL,
  `status` tinyint(1) NOT NULL,
  `reason` varchar(50) DEFAULT NULL,
  `updated_at` datetime NOT NULL,
  `updated_by` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`printer_id`));
  
  CREATE TABLE `product` (
  `product_id` varchar(50) NOT NULL,
  `name` varchar(50) NOT NULL,
  `image` varchar(100) NOT NULL,
  `description` varchar(300) NOT NULL,
  `original_price` decimal(5,2) NOT NULL,
  `original_GST` decimal(5,2) NOT NULL,
  `pwp_price` decimal(5,2) NOT NULL,
  `pwp_GST` decimal(5,2) NOT NULL,
  `quantity_constraint` varchar(50) DEFAULT NULL,
  `visibility` tinyint(1) NOT NULL,
  `photo_product` tinyint(1) NOT NULL,
  `updated_by` varchar(50) NOT NULL,
  `updated_at` datetime NOT NULL,
  PRIMARY KEY (`product_id`));
  
  CREATE TABLE `profiles` (
  `pid` varchar(50) NOT NULL,
  `profile` varchar(50) NOT NULL,
  `status` varchar(50) NOT NULL,
  PRIMARY KEY (`pid`));
  
  CREATE TABLE `role` (
  `role_id` int(2) NOT NULL AUTO_INCREMENT,
  `role` varchar(50) NOT NULL,
  PRIMARY KEY (`role_id`));
  
  CREATE TABLE `staff` (
  `staff_id` varchar(50) NOT NULL,
  `name` varchar(50) NOT NULL,
  `passwordhash` varchar(255) NOT NULL,
  `passwordsalt` varchar(255) NOT NULL,
  `role_id` int(2) NOT NULL,
  PRIMARY KEY (`staff_id`));
  
ALTER TABLE kidzania.email ADD FOREIGN KEY (uid) REFERENCES kidzania.profiles(pid);
ALTER TABLE kidzania.lineitem ADD FOREIGN KEY (order_id) REFERENCES kidzania.order(order_id);
ALTER TABLE kidzania.order ADD FOREIGN KEY (pid) REFERENCES kidzania.profiles(pid);
ALTER TABLE kidzania.item_photo ADD FOREIGN KEY (lineitem_id) REFERENCES kidzania.lineitem(lineitem_id);
ALTER TABLE kidzania.lineitem ADD FOREIGN KEY (product_id) REFERENCES kidzania.product(product_id);
ALTER TABLE kidzania.item_photo ADD FOREIGN KEY (assigned_printer_id) REFERENCES kidzania.printer(printer_id);
ALTER TABLE kidzania.printer ADD FOREIGN KEY (updated_by) REFERENCES kidzania.staff(staff_id);
ALTER TABLE kidzania.product ADD FOREIGN KEY (updated_by) REFERENCES kidzania.staff(staff_id);
ALTER TABLE kidzania.staff ADD FOREIGN KEY (role_id) REFERENCES kidzania.role(role_id);

INSERT INTO kidzania.role VALUES (1,'marketing');
INSERT INTO kidzania.staff VALUES ('staff1','john','abc','abc',1);
INSERT INTO kidzania.product VALUES ('1','Magnet','/Content/ProductPhoto/magnet.jpg','Phasellus non ante gravida, ultricies neque asdas',30.00,2.10,18.00,1.26,NULL,1,1,'staff1','2020-06-08 13:29:04'),('2','Keychain	','/Content/ProductPhoto/keychain.jpg','Phasellus non ante gravida, ultricies neque a',30.00,2.17,19.00,1.37,'Even Numbered Units',1,1,'staff1','2020-06-08 13:20:36'),('3','Establishment Card	','/Content/ProductPhoto/establishment card.jpg','Phasellus non ante gravida, ultricies neque a',30.00,1.24,19.00,1.24,NULL,1,1,'staff1','2020-01-19 03:14:07'),('4','A5 Hardcopy','/Content/ProductPhoto/a5 hardcopy.jpg','Phasellus non ante gravida, ultricies neque a',30.00,1.24,10.00,1.24,NULL,1,1,'staff1','2020-01-19 03:14:07'),('5','Leatherette','/Content/ProductPhoto/leatherette.png','Phasellus non ante gravida, ultricies neque a',30.00,1.24,11.00,1.24,NULL,1,0,'staff1','2020-01-19 03:14:07'),('6','Digital','/Content/ProductPhoto/digital.jpg','Phasellus non ante gravida, ultricies neque a',30.00,1.24,15.00,1.24,'Max 1 Unit 1 Digital',1,0,'staff1','2020-01-19 03:14:07');




