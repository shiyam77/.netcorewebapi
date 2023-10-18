
# . Altered Store procedure

ALTER PROCEDURE collegeapplications
    @nameFilter NVARCHAR(255) = NULL
AS
BEGIN
    -- Your SQL logic here
    IF @nameFilter IS NULL OR @nameFilter = ''
    BEGIN
        -- Handle the case when @nameFilter is not provided
        SELECT * FROM collegeapplicationsdata; -- Return all students
    END
    ELSE
    BEGIN
        -- Handle the case when @nameFilter is provided
        SELECT * FROM collegeapplicationsdata WHERE name = @nameFilter; -- Return filtered results
    END
END
