/*
字段不要有NULL值，可空的话，设置成默认为空字符串即可。
*/

drop table if exists User;

create table User
(
   Id       	int not null auto_increment comment ' 唯一标识',
   UserCode		varchar(20) not null comment '用户code',
   UserName		nvarchar(50) not null comment '用户名',
   Password		varchar(20) not null comment '密码',
   Phone		varchar(20) not null default '' comment '手机',
   Email		varchar(50) not null default '' comment '邮箱',
   Sex			int not null default -1 comment '性别：-1：未知，0：男，1：女',
   
   primary key(Id) ,
   unique (UserCode)
);

alter table User comment '用户';