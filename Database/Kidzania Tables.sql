use kidzania;

CREATE TABLE `product` (
  `ProductId` int(11) NOT NULL AUTO_INCREMENT,
  `ProductName` varchar(45) NOT NULL,
  `ProductDescription` varchar(1000) NOT NULL,
  `ProductImage` varchar(500) NOT NULL,
  `ProductVisibility` tinyint(1) NOT NULL,
  `CreatedBy` varchar(45) NOT NULL,
  `CreatedAt` datetime NOT NULL,
  `UpdatedBy` varchar(45) NOT NULL,
  `UpdatedAt` datetime NOT NULL,
  `PhotoProduct` tinyint(1) NOT NULL,
  PRIMARY KEY (`ProductId`)
);

INSERT INTO `product` VALUES (1,'Hardcopy','BLA BLA','BLA BLA',1,'Admin1','1000-01-01 00:00:00','Admin1','1000-01-01 00:00:00',1),(2,'Magnet','BLAA BLA','BLA BLA',1,'Admin2','1000-01-01 00:00:00','Admin1','1000-01-01 00:00:00',1),(3,'Keychain','BLa BLA','BLA BLA',1,'Admin1','1000-01-01 00:00:00','Admin2','1000-01-01 00:00:00',1),(4,'Digital','BLA BLA','BLA BLA',1,'Admin3','1000-01-01 00:00:00','Admin1','1000-01-01 00:00:00',1),(5,'Establishment Card','BLA BLA','BLA BLA',1,'Admin2','1000-01-01 00:00:00','Admin3','1000-01-01 00:00:00',1),(6,'Leatherette','BLA BLA ','BLA BLA',1,'Admin2','1000-01-01 00:00:00','Admin1','1000-01-01 00:00:00',0);

CREATE TABLE `product_price` (
  `ProductId` int(11) NOT NULL,
  `Unit` int(11) NOT NULL,
  `UnitPrice` decimal(10,2) NOT NULL,
  `UnitGST` decimal(10,2) NOT NULL,
  PRIMARY KEY (`ProductId`,`Unit`),
  CONSTRAINT `product_price_ibfk_1` FOREIGN KEY (`ProductId`) REFERENCES `product` (`ProductId`)
);

INSERT INTO `product_price` VALUES (1,1,25.00,2.00),(1,3,15.00,1.80),(1,5,12.00,1.60),(2,1,30.00,2.40),(3,2,12.50,2.00),(4,0,20.00,2.50),(5,1,8.00,1.50),(6,1,50.00,4.00);