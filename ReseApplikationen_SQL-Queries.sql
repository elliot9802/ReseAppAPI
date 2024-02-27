-- G. Show all Attractions without comments.
CREATE OR ALTER VIEW vw_AttractionsWithoutComments AS
SELECT a.*, l.City, l.Country, l.StreetAddress, c.Comment
FROM Attraction a
JOIN Location l ON a.LocationId = l.LocationId
LEFT JOIN Comment c ON a.AttractionId = c.AttractionId
WHERE c.AttractionId IS NULL;
GO;

-- G. Show all users and their comments.
CREATE OR ALTER VIEW vw_UsersAndComments AS
SELECT
    p.FirstName,
    p.LastName,
    c.Comment
FROM Person p
LEFT JOIN Comment c ON p.PersonId = c.PersonId;
GO;

-- VG. Create a Comment for an Attraction
CREATE OR ALTER PROCEDURE usp_AddCommentToAttraction (
  @AttractionID UNIQUEIDENTIFIER,
  @PersonId UNIQUEIDENTIFIER,
  @Comment NVARCHAR(250)
)
AS
BEGIN
DECLARE @CommentId UNIQUEIDENTIFIER = NEWID();
DECLARE @Seeded BIT = 0;
  INSERT INTO dbo.Comment (AttractionID, PersonId, CommentId, Comment, Seeded)
  VALUES (@AttractionID, @PersonId, @CommentID, @Comment, @Seeded);
END;
