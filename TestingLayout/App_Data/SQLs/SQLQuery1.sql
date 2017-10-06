

ALTER PROCEDURE spGetMenu
(
    @MenuCode VARCHAR(50),
    @LanguageCode VARCHAR(2)
)
AS
BEGIN
    SET NOCOUNT ON;

	SELECT	sl.Label, mi.URL
	FROM	Menu m,
			MenuItems mi,
			SystemLabels sl
	WHERE	m.ItemId = mi.Id
	AND		mi.LabelCode = sl.LabelCode
	AND		sl.LanguageCode = @LanguageCode
	AND		m.MenuCode = @MenuCode
END

EXEC spGetMenu 'home_main', 'en'

sp_helptext spGetLabels

select * from Users

SELECT	Sex,SexName 
FROM	GL_Sex
WHERE	[Language] = 'en' 


exec AddUser 'test', 't'



CREATE PROCEDURE spGetLabels
(
    @ViewModel VARCHAR(50),
    @LanguageCode VARCHAR(2)
)
AS
BEGIN
    SET NOCOUNT ON;
    
	SELECT	*
	FROM	SystemLabels

    insert into DataBase1.dbo.tableName values(@name,@address);
    
    insert into DataBase2.dbo.tableName(name,address) values(@name,@address)
    
END