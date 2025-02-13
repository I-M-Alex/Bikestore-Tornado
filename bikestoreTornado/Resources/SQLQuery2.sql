CREATE TABLE Quantity (
    BikeID INT NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (BikeID) REFERENCES Bikes(BikeID), 
    PRIMARY KEY (BikeID)
);