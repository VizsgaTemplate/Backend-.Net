CREATE DATABASE ingatlan
	CHARACTER SET utf8mb4
	COLLATE utf8mb4_hungarian_ci;

use ingatlan

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `kategoriak` (
    `id` int NOT NULL AUTO_INCREMENT,
    `nev` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_kategoriak` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ingatlanok` (
    `id` int NOT NULL AUTO_INCREMENT,
    `kategoriaid` int NOT NULL,
    `leiras` longtext CHARACTER SET utf8mb4 NOT NULL,
    `hirdetesDatuma` datetime(6) NOT NULL,
    `tehermentes` tinyint(1) NOT NULL,
    `ar` int NOT NULL,
    `kepUrl` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_ingatlanok` PRIMARY KEY (`id`),
    CONSTRAINT `FK_ingatlanok_kategoriak_kategoriaid` FOREIGN KEY (`kategoriaid`) REFERENCES `kategoriak` (`id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

INSERT INTO `kategoriak` (`id`, `nev`)
VALUES (1, 'Ház');
INSERT INTO `kategoriak` (`id`, `nev`)
VALUES (2, 'Lakás');
INSERT INTO `kategoriak` (`id`, `nev`)
VALUES (3, 'Építési telek');
INSERT INTO `kategoriak` (`id`, `nev`)
VALUES (4, 'Garázs');
INSERT INTO `kategoriak` (`id`, `nev`)
VALUES (5, 'Mezőgazdasági terület');
INSERT INTO `kategoriak` (`id`, `nev`)
VALUES (6, 'Ipari ingatlan');

CREATE INDEX `IX_ingatlanok_kategoriaid` ON `ingatlanok` (`kategoriaid`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230529140510_Initial', '6.0.7');

COMMIT;

