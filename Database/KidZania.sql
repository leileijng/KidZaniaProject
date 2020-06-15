-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)
--
-- Host: localhost    Database: kidzania
-- ------------------------------------------------------
-- Server version	5.7.11-log
use kidzania;

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `email`
--

DROP TABLE IF EXISTS `email`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `email` (
  `uid` varchar(50) NOT NULL,
  `email` varchar(255) NOT NULL,
  PRIMARY KEY (`uid`),
  CONSTRAINT `email_ibfk_1` FOREIGN KEY (`uid`) REFERENCES `profiles` (`pid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `email`
--

LOCK TABLES `email` WRITE;
/*!40000 ALTER TABLE `email` DISABLE KEYS */;
/*!40000 ALTER TABLE `email` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `item_photo`
--

DROP TABLE IF EXISTS `item_photo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `item_photo` (
  `itemphoto_id` varchar(999) NOT NULL,
  `photo` varchar(100) NOT NULL,
  `lineitem_id` varchar(10) NOT NULL,
  `assigned_printer_id` varchar(10) NOT NULL,
  `printing_status` varchar(10) NOT NULL,
  `updated_at` datetime NOT NULL,
  PRIMARY KEY (`itemphoto_id`),
  KEY `lineitem_id` (`lineitem_id`),
  KEY `assigned_printer_id` (`assigned_printer_id`),
  CONSTRAINT `item_photo_ibfk_1` FOREIGN KEY (`lineitem_id`) REFERENCES `lineitem` (`lineitem_id`),
  CONSTRAINT `item_photo_ibfk_2` FOREIGN KEY (`assigned_printer_id`) REFERENCES `printer` (`printer_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `item_photo`
--

LOCK TABLES `item_photo` WRITE;
/*!40000 ALTER TABLE `item_photo` DISABLE KEYS */;
/*!40000 ALTER TABLE `item_photo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lineitem`
--

DROP TABLE IF EXISTS `lineitem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lineitem` (
  `lineitem_id` varchar(10) NOT NULL,
  `order_id` varchar(10) NOT NULL,
  `product_id` varchar(50) NOT NULL,
  `photos` varchar(999) NOT NULL,
  `item_amount` decimal(5,2) NOT NULL,
  `status` varchar(10) NOT NULL,
  PRIMARY KEY (`lineitem_id`),
  KEY `order_id` (`order_id`),
  KEY `product_id` (`product_id`),
  CONSTRAINT `lineitem_ibfk_1` FOREIGN KEY (`order_id`) REFERENCES `order` (`order_id`),
  CONSTRAINT `lineitem_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `product` (`product_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lineitem`
--

LOCK TABLES `lineitem` WRITE;
/*!40000 ALTER TABLE `lineitem` DISABLE KEYS */;
/*!40000 ALTER TABLE `lineitem` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order`
--

DROP TABLE IF EXISTS `order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `order` (
  `order_id` varchar(10) NOT NULL,
  `pid` varchar(50) NOT NULL,
  `total_amount` decimal(5,2) NOT NULL,
  `status` varchar(10) NOT NULL,
  PRIMARY KEY (`order_id`),
  KEY `pid` (`pid`),
  CONSTRAINT `order_ibfk_1` FOREIGN KEY (`pid`) REFERENCES `profiles` (`pid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order`
--

LOCK TABLES `order` WRITE;
/*!40000 ALTER TABLE `order` DISABLE KEYS */;
/*!40000 ALTER TABLE `order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `printer`
--

DROP TABLE IF EXISTS `printer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `printer` (
  `printer_id` varchar(10) NOT NULL,
  `name` varchar(50) NOT NULL,
  `status` tinyint(1) NOT NULL,
  `reason` varchar(50) DEFAULT NULL,
  `updated_at` datetime NOT NULL,
  `updated_by` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`printer_id`),
  KEY `updated_by` (`updated_by`),
  CONSTRAINT `printer_ibfk_1` FOREIGN KEY (`updated_by`) REFERENCES `staff` (`staff_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `printer`
--

LOCK TABLES `printer` WRITE;
/*!40000 ALTER TABLE `printer` DISABLE KEYS */;
/*!40000 ALTER TABLE `printer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
  PRIMARY KEY (`product_id`),
  KEY `updated_by` (`updated_by`),
  CONSTRAINT `product_ibfk_1` FOREIGN KEY (`updated_by`) REFERENCES `staff` (`staff_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES ('1','Magnet','/Content/ProductPhoto/magnet.jpg','Phasellus non ante gravida, ultricies neque asdas',30.00,2.10,18.00,1.26,NULL,1,1,'staff1','2020-06-08 13:29:04'),('2','Keychain	','/Content/ProductPhoto/keychain.jpg','Phasellus non ante gravida, ultricies neque a',30.00,2.17,19.00,1.37,'Even Numbered Units',1,1,'staff1','2020-06-08 13:20:36'),('3','Establishment Card	','/Content/ProductPhoto/card.jpg','Phasellus non ante gravida, ultricies neque a',30.00,1.24,19.00,1.24,NULL,1,1,'staff1','2020-01-19 03:14:07'),('4','A5 Hardcopy','/Content/ProductPhoto/a5.jpg','Phasellus non ante gravida, ultricies neque a',30.00,1.24,10.00,1.24,NULL,1,1,'staff1','2020-01-19 03:14:07'),('5','Leatherette','/Content/ProductPhoto/leatherette.png','Phasellus non ante gravida, ultricies neque a',30.00,1.24,11.00,1.24,NULL,1,0,'staff1','2020-01-19 03:14:07'),('6','Digital','/Content/ProductPhoto/digital.jpg','Phasellus non ante gravida, ultricies neque a',30.00,1.24,15.00,1.24,'Max 1 Unit 1 Digital',1,0,'staff1','2020-01-19 03:14:07');
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `profiles`
--

DROP TABLE IF EXISTS `profiles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `profiles` (
  `pid` varchar(50) NOT NULL,
  `profile` varchar(50) NOT NULL,
  `status` varchar(50) NOT NULL,
  PRIMARY KEY (`pid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `profiles`
--

LOCK TABLES `profiles` WRITE;
/*!40000 ALTER TABLE `profiles` DISABLE KEYS */;
/*!40000 ALTER TABLE `profiles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role` (
  `role_id` int(2) NOT NULL AUTO_INCREMENT,
  `role` varchar(50) NOT NULL,
  PRIMARY KEY (`role_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'marketing');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `staff`
--

DROP TABLE IF EXISTS `staff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `staff` (
  `staff_id` varchar(50) NOT NULL,
  `name` varchar(50) NOT NULL,
  `passwordhash` varchar(255) NOT NULL,
  `passwordsalt` varchar(255) NOT NULL,
  `role_id` int(2) NOT NULL,
  PRIMARY KEY (`staff_id`),
  KEY `role_id` (`role_id`),
  CONSTRAINT `staff_ibfk_1` FOREIGN KEY (`role_id`) REFERENCES `role` (`role_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `staff`
--

LOCK TABLES `staff` WRITE;
/*!40000 ALTER TABLE `staff` DISABLE KEYS */;
INSERT INTO `staff` VALUES ('staff1','john','abc','abc',1);
/*!40000 ALTER TABLE `staff` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-06-08 19:32:05
