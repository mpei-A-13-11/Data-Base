use ITA_Support
go

declare @courceNumber int;

set @courceNumber = 1;

while @courceNumber < 500
begin

insert into Cources values(@courceNumber, CAST ((1000-@courceNumber) as varchar(3)) + 'A', '���������� ��� ������ � Microsoft SQL Server 2008', '���������� ��� ������������ ����������� ����������� ���� ���������� ���������� ������ � ������ � ������� ��������� ���� Microsoft SQL Server 2008. ����� ����� �������� ������� ���������� ������������� ������������ � ������� �������� SQL Server 2008, ��������������� ��� ��������� ����.');

set @courceNumber = @courceNumber + 1;

end;


set @courceNumber = 1;

while @courceNumber < 500
begin


delete from Cources where Id = @courceNumber;

set @courceNumber = @courceNumber + 3;

end;
