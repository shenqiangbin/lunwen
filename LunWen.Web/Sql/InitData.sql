insert user(UserCode,UserName,Salt,Password) values('admin','管理员','c667c7fb-fd99-4576-8f6d-8ac10e930514','96656905C76EFD9B4734106875CD8A59CCD495187A1BCDD88C9756632C48AA282527D58FCA9711F4BE6C7A1CB85ABDF70DAE0321B47B0B66E774CD0F92262837');
insert user(UserCode,UserName,Salt,Password) values('user001','用户001','34964fb6-f956-4183-b9ee-4ce403c70da8','219B046538158219C9996A2C4C7001B4868B4EF3704C05ECABD37A456C1AECAB50EBF3B9A4A21B5866177E6F734C28B6E209607524D7FCD366AF108849AE2BB5');
insert user(UserCode,UserName,Salt,Password) values('user002','用户002','c667c7fb-fd99-4576-8f6d-8ac10e930514','96656905C76EFD9B4734106875CD8A59CCD495187A1BCDD88C9756632C48AA282527D58FCA9711F4BE6C7A1CB85ABDF70DAE0321B47B0B66E774CD0F92262837');
insert user(UserCode,UserName,Salt,Password) values('user003','用户003','c667c7fb-fd99-4576-8f6d-8ac10e930514','96656905C76EFD9B4734106875CD8A59CCD495187A1BCDD88C9756632C48AA282527D58FCA9711F4BE6C7A1CB85ABDF70DAE0321B47B0B66E774CD0F92262837');
insert user(UserCode,UserName,Salt,Password) values('user004','用户004','c667c7fb-fd99-4576-8f6d-8ac10e930514','96656905C76EFD9B4734106875CD8A59CCD495187A1BCDD88C9756632C48AA282527D58FCA9711F4BE6C7A1CB85ABDF70DAE0321B47B0B66E774CD0F92262837');
insert user(UserCode,UserName,Salt,Password) values('user005','用户005','c667c7fb-fd99-4576-8f6d-8ac10e930514','96656905C76EFD9B4734106875CD8A59CCD495187A1BCDD88C9756632C48AA282527D58FCA9711F4BE6C7A1CB85ABDF70DAE0321B47B0B66E774CD0F92262837');
insert user(UserCode,UserName,Salt,Password) values('user006','用户006','c667c7fb-fd99-4576-8f6d-8ac10e930514','96656905C76EFD9B4734106875CD8A59CCD495187A1BCDD88C9756632C48AA282527D58FCA9711F4BE6C7A1CB85ABDF70DAE0321B47B0B66E774CD0F92262837');
insert user(UserCode,UserName,Salt,Password) values('user007','用户007','c667c7fb-fd99-4576-8f6d-8ac10e930514','96656905C76EFD9B4734106875CD8A59CCD495187A1BCDD88C9756632C48AA282527D58FCA9711F4BE6C7A1CB85ABDF70DAE0321B47B0B66E774CD0F92262837');
insert user(UserCode,UserName,Salt,Password) values('user008','用户008','c667c7fb-fd99-4576-8f6d-8ac10e930514','96656905C76EFD9B4734106875CD8A59CCD495187A1BCDD88C9756632C48AA282527D58FCA9711F4BE6C7A1CB85ABDF70DAE0321B47B0B66E774CD0F92262837');

INSERT INTO `thesisdb`.`userrole` (`UserId`, `RoleId`, `Status`) VALUES ('1', '1', '1');

INSERT INTO `thesisdb`.`role` (`RoleCode`, `RoleName`, `Level`, `Status`) VALUES ('SuperAdmin', '超级管理员', '1', '1');
INSERT INTO `thesisdb`.`role` (`RoleCode`, `RoleName`, `Level`, `Status`) VALUES ('CoPerAdmin', '合作部管理员', '2', '1');
INSERT INTO `thesisdb`.`role` (`RoleCode`, `RoleName`, `Level`, `Status`) VALUES ('SchoolAdmin', '学校管理员', '2', '1');


INSERT INTO `thesisdb`.`menu` (`Id`,`ParentId`, `Level`, `MenuUrl`, `MenuName`, `Sort`, `Status`) VALUES ('1','0', '1', '/Manager/Index', '首页', '1', '1');
INSERT INTO `thesisdb`.`menu` (`Id`,`ParentId`, `Level`, `MenuUrl`, `MenuName`, `Sort`, `Status`) VALUES ('2','0', '1', '/ThesisMgr/Index', '论文管理','2',  '1');
INSERT INTO `thesisdb`.`menu` (`Id`,`ParentId`, `Level`, `MenuUrl`, `MenuName`, `Sort`, `Status`) VALUES ('3','0', '1', '/CostMgr/Index', '费用管理', '3', '1');
INSERT INTO `thesisdb`.`menu` (`Id`,`ParentId`, `Level`, `MenuUrl`, `MenuName`, `Sort`, `Status`) VALUES ('4','0', '1', '/NoticeMgr/Index', '通知公告', '4', '1');
INSERT INTO `thesisdb`.`menu` (`Id`,`ParentId`, `Level`, `MenuUrl`, `MenuName`, `Sort`, `Status`) VALUES ('5','0', '1', '/OrgMgr/Index', '架构管理', '5', '1');


INSERT INTO `thesisdb`.`menu` (`Id`,`ParentId`, `Level`, `MenuUrl`, `MenuName`, `Sort`, `Status`) VALUES ('6','2', '2', '/ThesisMgr/Check', '论文审核', '1', '1');
INSERT INTO `thesisdb`.`menu` (`Id`,`ParentId`, `Level`, `MenuUrl`, `MenuName`, `Sort`, `Status`) VALUES ('7','2', '2', '/ThesisMgr/BatchSubmit', '批量提交', '2', '1');
INSERT INTO `thesisdb`.`menu` (`Id`,`ParentId`, `Level`, `MenuUrl`, `MenuName`, `Sort`, `Status`) VALUES ('8','2', '2', '/ThesisMgr/Feedback', '加工反馈','3',  '1');
INSERT INTO `thesisdb`.`menu` (`Id`,`ParentId`, `Level`, `MenuUrl`, `MenuName`, `Sort`, `Status`) VALUES ('9','2', '2', '/ThesisMgr/Rewards', '薪酬管理','4',  '1');


/*管理员有所有权限*/
INSERT INTO `thesisdb`.`rolemenu` (`RoleId`, `MenuId`, `Status`) VALUES ('1', '1', '1');
INSERT INTO `thesisdb`.`rolemenu` (`RoleId`, `MenuId`, `Status`) VALUES ('1', '2', '1');
INSERT INTO `thesisdb`.`rolemenu` (`RoleId`, `MenuId`, `Status`) VALUES ('1', '3', '1');
INSERT INTO `thesisdb`.`rolemenu` (`RoleId`, `MenuId`, `Status`) VALUES ('1', '4', '1');
INSERT INTO `thesisdb`.`rolemenu` (`RoleId`, `MenuId`, `Status`) VALUES ('1', '5', '1');
INSERT INTO `thesisdb`.`rolemenu` (`RoleId`, `MenuId`, `Status`) VALUES ('1', '6', '1');
INSERT INTO `thesisdb`.`rolemenu` (`RoleId`, `MenuId`, `Status`) VALUES ('1', '7', '1');
INSERT INTO `thesisdb`.`rolemenu` (`RoleId`, `MenuId`, `Status`) VALUES ('1', '8', '1');
INSERT INTO `thesisdb`.`rolemenu` (`RoleId`, `MenuId`, `Status`) VALUES ('1', '9', '1');


/*合作部管理员没有（首页、批量提交、架构管理）*/
INSERT INTO `thesisdb`.`rolemenu` (`RoleId`, `MenuId`, `Status`) VALUES ('2', '2', '1');
INSERT INTO `thesisdb`.`rolemenu` (`RoleId`, `MenuId`, `Status`) VALUES ('2', '3', '1');
INSERT INTO `thesisdb`.`rolemenu` (`RoleId`, `MenuId`, `Status`) VALUES ('2', '4', '1');

INSERT INTO `thesisdb`.`rolemenu` (`RoleId`, `MenuId`, `Status`) VALUES ('2', '6', '1');
INSERT INTO `thesisdb`.`rolemenu` (`RoleId`, `MenuId`, `Status`) VALUES ('2', '8', '1');
INSERT INTO `thesisdb`.`rolemenu` (`RoleId`, `MenuId`, `Status`) VALUES ('2', '9', '1');

INSERT INTO `thesisdb`.`accessconfig` (`AppKey`, `AppSecret`, `Status`) VALUES ('44j2scyyl4rdrtaj4cdm0f', 's+QG+0CIX0G0T22pw+I+jw', '1');