create database if not exists poster;
use poster;

drop table if exists announcement;

create table announcement (
	announcId int auto_increment not Null,
	`name` varchar(200),
    title varchar(200) DEFAULT NULL,
    `description` varchar(300) DEFAULT NULL,
    dateAdd DATE,
    Primary key(announcId)
)ENGINE = InnoDB;

Select * 
From announcement;

INSERT INTO `poster`.`announcement` 
(`name`, `title`, `description`, `dateAdd`) 
VALUES ('News', 'just simple news which you can see everywhere', 'just simple news which you can see everywhere', '2021.07.30');

use poster;

/*Запит на кількість рядків у таблиці*/
Select count(announcId) 
From announcement;

Select announcId, title, `description`
From announcement;

/*Procedures */
/*For delete*/
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `announcDelete`(
	_announcId int
)
BEGIN 
	DELETE FROM poster.announcement
    WHERE announcId = _announcId;
END
delimiter;

/*AddOrEdit*/
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `posterAddorEdit`(
    _announcementID int,
	_name varchar(200),
    _title varchar(200) ,
    _description varchar(300) ,
    _dateAdd DATE
)
BEGIN
	if _announcementID = 0 then 
    INSERT INTO announcement( `name`, title, `description`,dateAdd)
    values(`_name`, _title, `_description`, _dateAdd);
    else 
		update announcement
        SET
        `name` = _name,
        title = _title,
        `description` = _description,
        dateAdd = _dateAdd
	 WHERE announcId = _announcementID;
     end if; 
END
delimiter;

/*posterSummary - FindCount of announcement*/
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `posterSummary`()
BEGIN 
	SELECT * FROM announcement;
END;
delimiter;

/*PosterViewAll*/
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `PosterViewAll`()
BEGIN 
	select * 
    From announcement;
END;
delimiter;

/*viewByid*/
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `viewById`(
	_announcId int
)
BEGIN
	Select *
    from announcement
    where announcId = _announcId;
END;
delimiter;

/*viewTop*/
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `viewTop`(
)
BEGIN
	Select announcId, title, `description` 
    From announcement;
END;
delimiter;
/*-------------*/

