/*
字段不要有NULL值，可空的话，设置成默认为空字符串即可。
*/
drop table if exists log;
CREATE TABLE `log` (
          `id` int(11) NOT NULL AUTO_INCREMENT,
          `date` datetime DEFAULT NULL,
          `thread` varchar(45) DEFAULT NULL,
          `level` varchar(45) DEFAULT NULL,
          `logger` varchar(45) DEFAULT NULL,
          `exception` varchar(45) DEFAULT NULL,
          `message` varchar(4000) DEFAULT NULL,
          `userid` varchar(45) DEFAULT NULL,
          PRIMARY KEY (`id`)
        ) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8;
alter table log comment '错误日志';

drop table if exists timelog;
CREATE TABLE `timelog` (
          `id` int(11) NOT NULL AUTO_INCREMENT,
          `date` datetime DEFAULT NULL,
          `thread` varchar(45) DEFAULT NULL,
          `level` varchar(45) DEFAULT NULL,
          `logger` varchar(45) DEFAULT NULL,
          `exception` varchar(45) DEFAULT NULL,
          `message` varchar(4000) DEFAULT NULL,
		  `usercode` varchar(100) DEFAULT NULL,
          `actionUrl` varchar(100) DEFAULT NULL,
		  `actionElapsed` varchar(100) DEFAULT NULL,
		  `renderElapsed` varchar(100) DEFAULT NULL,
		  `paraObj` varchar(2000) DEFAULT NULL,
          PRIMARY KEY (`id`)
        ) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8;
alter table timelog comment '方法调用用时日志';

drop table if exists User;
create table User
(
   Id       	int not null auto_increment comment ' 唯一标识',
   UserCode		varchar(20) not null comment '用户code',
   UserName		nvarchar(50) not null comment '用户名',
   Password		varchar(200) not null comment '密码',
   Salt			varchar(37) not null comment '盐',
   Phone		varchar(20) not null default '' comment '手机',
   Email		varchar(50) not null default '' comment '邮箱',
   Sex			int not null default -1 comment '性别：-1：未知，0：男，1：女',
   Status		int not null default 1 comment '是否删除：1：未删除，0：已删除',
   
   primary key(Id) ,
   unique (UserCode)
);

alter table User comment '用户';


drop table if exists Role;
create table Role
(
   Id       	int not null auto_increment comment ' 唯一标识',
   RoleCode		varchar(20) not null comment '角色code',
   RoleName		nvarchar(50) not null comment '名称',
   Level		int not null default -1 comment '级别',
   Status		int not null default 1 comment '是否删除：1：未删除，0：已删除',
   
   primary key(Id) ,
   unique (RoleCode),
   unique (RoleName)
);
alter table Role comment '角色';

drop table if exists UserRole;
create table UserRole
(
   Id       	int not null auto_increment comment ' 唯一标识',
   UserId		int not null comment '用户id',
   RoleId		int not null comment '角色id',   
   Status		int not null default 1 comment '是否删除：1：未删除，0：已删除',
   
   primary key(Id)    
);
alter table UserRole comment '用户&角色';


drop table if exists RoleMenu;
create table RoleMenu
(
   Id       	int not null auto_increment comment ' 唯一标识',
   RoleId		int not null comment '角色id',
   MenuId		int not null comment '菜单id',   
   Status		int not null default 1 comment '是否删除：1：未删除，0：已删除',
   
   primary key(Id) 
);
alter table RoleMenu comment '角色&菜单';

drop table if exists Menu;
create table Menu
(
   Id       	int not null auto_increment comment ' 唯一标识',
   ParentId		int not null comment '父节点',
   Level		int not null comment '级别',
   MenuUrl		varchar(50) not null comment 'URL地址',
   MenuName		nvarchar(50) not null comment '菜单名称',   
   Sort			int not null comment '排序',
   Status		int not null default 1 comment '是否删除：1：未删除，0：已删除',
   
   primary key(Id)    
);
alter table Menu comment '菜单';

drop table if exists accessconfig;
create table accessconfig
(
   Id       	int not null auto_increment comment ' 唯一标识',
   AppKey		varchar(100) not null comment 'appKey',
   AppSecret	varchar(100) not null comment '密钥',   
   Status		int not null default 1 comment '是否删除：1：未删除，0：已删除',
   
   primary key(Id) ,
   unique (AppKey),
   unique (AppSecret)
);
alter table accessconfig comment 'api访问控制表';


drop table if exists smsTask;
create table smsTask
(
	id			int not null auto_increment comment '唯一标识',
    sysid		int not null comment '系统id',
    sysname 	nvarchar(50) not null comment '系统名称',
    moduleid	int not null comment '模块id',
    modulename	nvarchar(50) not null comment '模块名称',
    phone 		varchar(50) not null default '' comment '手机号',
    text		nvarchar(50) not null comment '短信内容',
    status		int not null default 0 comment '状态：0待发送，1发送成功，2发送失败',
    date		datetime not null comment '创建日期',     
    
    primary key(id)
);
alter table smsTask comment '短信任务';

drop table if exists smsTaskLog;
create table smsTaskLog
(
   id       	int not null auto_increment comment '唯一标识',
   sysid		int not null comment '系统id',
   sysname		nvarchar(50) not null comment '系统名称',
   moduleid		int not null comment '模块id',
   modulename	nvarchar(50) not null comment '模块名称',
   date			datetime not null comment '发送日期',
   phone		varchar(50) not null default '' comment '手机号',
   text			nvarchar(50) not null comment '短信内容',
   success		int not null default -1 comment '是否成功：1：成功，0：失败',
   smschannel	nvarchar(50) not null default 1 comment '短信通道',
   msg			nvarchar(150) null default '' comment '其他信息',
   
   primary key(id)
);

alter table smsTaskLog comment '发送短信任务日志';