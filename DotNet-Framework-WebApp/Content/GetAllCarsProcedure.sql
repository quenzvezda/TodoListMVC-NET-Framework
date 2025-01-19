ALTER PROCEDURE GetAllCars
AS
BEGIN
    SELECT 
        c.Id,
        c.Brand,
        c.Color,
        COUNT(t.Id) AS TireCount -- Hitung jumlah Tire
    FROM 
        Cars c
    LEFT JOIN 
        Tires t ON c.Id = t.CarId
    GROUP BY 
        c.Id, c.Brand, c.Color -- Kolom yang di-SELECT harus dikelompokkan
END
