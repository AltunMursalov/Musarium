USE [MuzariumFull]
GO
/****** Object:  StoredProcedure [dbo].[UpdateMuseum]    Script Date: 13.06.2018 1:06:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateMuseum] @cityName nvarchar(50), @address nvarchar(50), @description nvarchar(300), @title nvarchar(30), 
@latitude float, @longitude float, @phone nvarchar(20), @radius int, @webSite nvarchar(50), @id int
AS
BEGIN
	DECLARE @cityId int;
	IF EXISTS(SELECT Cities.Id FROM Cities WHERE CityName = @cityName)
	BEGIN
		SELECT @cityId = Id FROM Cities WHERE CityName = @cityName
		UPDATE Museums
		SET [Address] = @address, [Description] = @description, Latitude = @latitude, Longitude = @longitude, Phone = @phone,
		Radius = @radius, WebSite = @webSite, Title = @title, CityId = @cityId WHERE Id = @id
	END
	ELSE
	BEGIN
		INSERT INTO Cities(CityName) VALUES(@cityName)
		SELECT @cityId = Id FROM Cities WHERE CityName = @cityName
		UPDATE Museums
		SET [Address] = @address, [Description] = @description, Latitude = @latitude, Longitude = @longitude, Phone = @phone,
		Radius = @radius, WebSite = @webSite, Title = @title, CityId = @cityId WHERE Id = @id
	END
END