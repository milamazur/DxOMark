DROP TABLE IF EXISTS DxoMark.TestedDevice;
DROP TABLE IF EXISTS DxoMark.Summary;
DROP TABLE IF EXISTS DxoMark.DeviceMeasurement;
DROP TABLE IF EXISTS DxoMark.DeviceFunctionality;
DROP TABLE IF EXISTS DxoMark.DeviceSpecification;
DROP TABLE IF EXISTS DxoMark.ArticleDevice;
DROP TABLE IF EXISTS DxoMark.Device;
DROP TABLE IF EXISTS DxoMark.Specification;
DROP TABLE IF EXISTS DxoMark.ArticleCategory;
DROP TABLE IF EXISTS DxoMark.Brand;
DROP TABLE IF EXISTS DxoMark.MeasurementType;
DROP TABLE IF EXISTS DxoMark.Functionality;
DROP TABLE IF EXISTS DxoMark.SpecificationType;
DROP TABLE IF EXISTS DxoMark.Article;
DROP TABLE IF EXISTS DxoMark.Category;

DROP SCHEMA IF EXISTS DxoMark;
GO

CREATE SCHEMA DxoMark;
GO

CREATE TABLE DxoMark.Category
(
    id int IDENTITY(1,1),
    [name] varchar(50) NOT NULL,
    CONSTRAINT PK_Category  PRIMARY KEY (id)
);

CREATE TABLE DxoMark.Article
(
    id int IDENTITY(1,1),
    title varchar(100) NOT NULL,
    [text] text NOT NULL,
    [image] varchar(200) NULL,
    [video] varchar(200) NULL,
    uploadDate date NULL,
    CONSTRAINT PK_Article  PRIMARY KEY (id)
);

CREATE TABLE DxoMark.SpecificationType
(
    id int IDENTITY(1,1),
    [name] varchar(50) NOT NULL,
    CONSTRAINT PK_SpecificationType  PRIMARY KEY (id)
);

CREATE TABLE DxoMark.Functionality
(
    id int IDENTITY(1,1),
    [name] varchar(50) NOT NULL,
    CONSTRAINT PK_Functionality  PRIMARY KEY (id)
);

CREATE TABLE DxoMark.MeasurementType
(
    id int IDENTITY(1,1),
    [name] varchar(50) NOT NULL,
    CONSTRAINT PK_MeasurementType  PRIMARY KEY (id)
);

CREATE TABLE DxoMark.Brand
(
    id int IDENTITY(1,1),
    [name] varchar(20) NOT NULL,
    CONSTRAINT PK_Brand  PRIMARY KEY (id)
);

CREATE TABLE DxoMark.ArticleCategory
(
    id int IDENTITY(1,1),
    categoryID int NOT NULL,
    articleID int NOT NULL,
    CONSTRAINT PK_ArticleCategory  PRIMARY KEY (id),
    CONSTRAINT FK_ArticleCategory_Category FOREIGN KEY (categoryID) REFERENCES DxoMark.Category(id),
    CONSTRAINT FK_ArticleCategory_Article FOREIGN KEY (articleID) REFERENCES DxoMark.Article(id) ON DELETE CASCADE
);

CREATE TABLE DxoMark.Specification
(
    id int IDENTITY(1,1),
    [name] varchar(50) NOT NULL,
    displayTop bit NOT NULL,
    specificationTypeID int NOT NULL,
    CONSTRAINT PK_Specification  PRIMARY KEY (id),
    CONSTRAINT FK_Specification_SpecificationType FOREIGN KEY (specificationTypeID) REFERENCES DxoMark.SpecificationType(id)
);

CREATE TABLE DxoMark.Device
(
    id int IDENTITY(1,1),
    [name] varchar(50) NOT NULL,
    launchPrice money NOT NULL,
    launchDate date NOT NULL,
    review text NULL,
    [video] varchar(100) NULL,
    [image] varchar(200) NOT NULL,
    brandID int NOT NULL,
    CONSTRAINT PK_Device  PRIMARY KEY (id),
    CONSTRAINT FK_Device_Brand FOREIGN KEY (brandID) REFERENCES DxoMark.Brand(id)
);

CREATE TABLE DxoMark.ArticleDevice
(
    id int IDENTITY(1,1),
    deviceID int NOT NULL,
    articleID int NOT NULL,
    CONSTRAINT PK_ArticleDevice  PRIMARY KEY (id),
    CONSTRAINT FK_ArticleDevice_Device FOREIGN KEY (deviceID) REFERENCES DxoMark.Device(id),
    CONSTRAINT FK_ArticleDevice_Article FOREIGN KEY (articleID) REFERENCES DxoMark.Article(id) ON DELETE CASCADE
);


CREATE TABLE DxoMark.DeviceSpecification
(
    id int IDENTITY(1,1),
    [value] varchar(50) NULL,
    deviceID int NOT NULL,
    specificationID int NOT NULL,
    CONSTRAINT PK_DeviceSpecification  PRIMARY KEY (id),
    CONSTRAINT FK_DeviceSpecification_Device FOREIGN KEY (deviceID) REFERENCES DxoMark.Device(id) ON DELETE CASCADE,
    CONSTRAINT FK_DeviceSpecification_Specification FOREIGN KEY (specificationID) REFERENCES DxoMark.Specification(id)
);

CREATE TABLE DxoMark.DeviceFunctionality
(
    id int IDENTITY(1,1),
    score decimal(7,1) NULL,
    deviceID int NOT NULL,
    functionalityID int NOT NULL,
    CONSTRAINT PK_DeviceFunctionality  PRIMARY KEY (id),
    CONSTRAINT FK_DeviceFunctionality_Device FOREIGN KEY (deviceID) REFERENCES DxoMark.Device(id) ON DELETE CASCADE,
    CONSTRAINT FK_DeviceFunctionality_Functionality FOREIGN KEY (functionalityID) REFERENCES DxoMark.Functionality(id)
);

CREATE TABLE DxoMark.DeviceMeasurement
(
    id int IDENTITY(1,1),
    infographic varchar(200) NOT NULL,
    deviceID int NOT NULL,
    measurementTypeID int NOT NULL,
    CONSTRAINT PK_DeviceMeasurement  PRIMARY KEY (id),
    CONSTRAINT FK_DeviceMeasurement_Device FOREIGN KEY (deviceID) REFERENCES DxoMark.Device(id) ON DELETE CASCADE,
    CONSTRAINT FK_DeviceMeasurement_MeasurementType FOREIGN KEY (measurementTypeID) REFERENCES DxoMark.MeasurementType(id)
);

CREATE TABLE DxoMark.Summary
(
    id int IDENTITY(1,1),
    [text] text NULL,
    isPro bit NOT NULL,
    deviceID int NOT NULL,
    CONSTRAINT PK_Summary  PRIMARY KEY (id),
    CONSTRAINT FK_Summary_Device FOREIGN KEY (deviceID) REFERENCES DxoMark.Device(id) ON DELETE CASCADE
);


CREATE TABLE DxoMark.TestedDevice
(
    id int IDENTITY(1,1),
    deviceTestedID int NOT NULL,
    deviceTestingID int NOT NULL,
    CONSTRAINT PK_TestedDevice  PRIMARY KEY (id),
    CONSTRAINT FK_TestedDevice_Device_Tested FOREIGN KEY (deviceTestedID) REFERENCES DxoMark.Device(id) ON DELETE CASCADE,
    CONSTRAINT FK_TestedDevice_Device_Testing FOREIGN KEY (deviceTestingID) REFERENCES DxoMark.Device(id)
);