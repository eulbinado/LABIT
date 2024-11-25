-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 25, 2024 at 03:23 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `computer_laboratory_management_system`
--

-- --------------------------------------------------------

--
-- Table structure for table `assets`
--

CREATE TABLE `assets` (
  `asset_id` varchar(10) NOT NULL,
  `asset_type` varchar(50) NOT NULL,
  `asset_brand` varchar(50) NOT NULL,
  `asset_description` varchar(100) NOT NULL,
  `location` varchar(20) NOT NULL DEFAULT 'Unassigned',
  `system_unit` varchar(20) NOT NULL DEFAULT 'Unequipped',
  `workstation` varchar(20) NOT NULL DEFAULT 'Unequipped',
  `status` varchar(20) NOT NULL DEFAULT 'Working',
  `date_added` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `assets`
--

INSERT INTO `assets` (`asset_id`, `asset_type`, `asset_brand`, `asset_description`, `location`, `system_unit`, `workstation`, `status`, `date_added`) VALUES
('A0011', 'AVR', 'Luminous', '600VA, surge protection, compact design.', 'Lab 3', 'Unequipped', 'W001', 'Working', '2024-10-29 23:08:09'),
('A0023', 'AVR', 'Luminous', '600VA, surge protection, compact design.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-30 17:38:53'),
('A0024', 'AVR', 'Luminous', '600VA, surge protection, compact design.', 'Lab 3', 'Unequipped', 'W002', 'Working', '2024-10-30 17:38:53'),
('C0016', 'CPU', 'AMD', 'Athlon 3000G, dual-core, 3.5GHz.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:09:47'),
('C0017', 'CPU', 'AMD', 'Athlon 3000G, dual-core, 3.5GHz.', 'Lab 3', 'SU22', 'Unequipped', 'Working', '2024-10-29 23:09:47'),
('C0018', 'CPU', 'AMD', 'Athlon 3000G, dual-core, 3.5GHz.', 'Lab 4', 'SU23', 'Unequipped', 'Working', '2024-10-29 23:09:47'),
('C0019', 'CPU', 'AMD', 'Athlon 3000G, dual-core, 3.5GHz.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:09:47'),
('G0023', 'GPU', 'NVIDIA GeForce GT 730', '2GB DDR3', 'Lab 4', 'SU23', 'Unequipped', 'Working', '2024-10-29 23:16:31'),
('G0024', 'GPU', 'NVIDIA GeForce GT 730', '2GB DDR3', 'Lab 3', 'SU22', 'Unequipped', 'Working', '2024-10-29 23:16:31'),
('G0025', 'GPU', 'NVIDIA GeForce GT 730', '2GB DDR3', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:16:31'),
('G0026', 'GPU', 'NVIDIA GeForce GT 730', '2GB DDR3', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:16:31'),
('K0001', 'Keyboard', 'Zebronics', 'Wired, membrane keys, basic design.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:06:45'),
('K0002', 'Keyboard', 'Zebronics', 'Wired, membrane keys, basic design.', 'Lab 3', 'Unequipped', 'W001', 'Working', '2024-10-29 23:06:45'),
('K0003', 'Keyboard', 'Zebronics', 'Wired, membrane keys, basic design.', 'Lab 3', 'Unequipped', 'W002', 'Working', '2024-10-29 23:06:45'),
('K0004', 'Keyboard', 'Zebronics', 'Wired, membrane keys, basic design.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:06:45'),
('K0005', 'Keyboard', 'Zebronics', 'Wired, membrane keys, basic design.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:06:45'),
('M0011', 'Monitor', 'AOC', '18.5-inch, 1366 x 768 resolution, LED backlight.', 'Lab 3', 'Unequipped', 'Unequipped', 'Replacement', '2024-10-29 23:07:39'),
('M0012', 'Monitor', 'AOC', '18.5-inch, 1366 x 768 resolution, LED backlight.', 'Lab 3', 'Unequipped', 'W002', 'Working', '2024-10-29 23:07:39'),
('M0013', 'Monitor', 'AOC', '18.5-inch, 1366 x 768 resolution, LED backlight.', 'Lab 3', 'Unequipped', 'W001', 'Working', '2024-10-29 23:07:39'),
('M0014', 'Monitor', 'AOC', '18.5-inch, 1366 x 768 resolution, LED backlight.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:07:39'),
('M0015', 'Monitor', 'AOC', '18.5-inch, 1366 x 768 resolution, LED backlight.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:07:39'),
('MB0011', 'Motherboard', 'Gigabyte', 'Micro ATX, Intel LGA 1151 socket, basic features.', 'Lab 4', 'SU23', 'Unequipped', 'Working', '2024-10-29 23:08:45'),
('MB0012', 'Motherboard', 'Gigabyte', 'Micro ATX, Intel LGA 1151 socket, basic features.', 'Lab 3', 'SU22', 'Unequipped', 'Working', '2024-10-29 23:08:45'),
('MB0013', 'Motherboard', 'Gigabyte', 'Micro ATX, Intel LGA 1151 socket, basic features.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:08:45'),
('MB0014', 'Motherboard', 'Gigabyte', 'Micro ATX, Intel LGA 1151 socket, basic features.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:08:45'),
('MB0060', 'Motherboard', 'Gigabyte', 'Micro ATX, Intel LGA 1151 socket, basic features.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:08:45'),
('MO0006', 'Mouse', 'A4 Tech', 'Wired optical mouse, simple design.', 'Lab 3', 'Unequipped', 'W001', 'Working', '2024-10-29 23:07:18'),
('MO0007', 'Mouse', 'A4 Tech', 'Wired optical mouse, simple design.', 'Lab 3', 'Unequipped', 'W002', 'Working', '2024-10-29 23:07:18'),
('MO0008', 'Mouse', 'A4 Tech', 'Wired optical mouse, simple design.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:07:18'),
('MO0009', 'Mouse', 'A4 Tech', 'Wired optical mouse, simple design.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:07:18'),
('MO0010', 'Mouse', 'A4 Tech', 'Wired optical mouse, simple design.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:07:18'),
('P0016', 'PSU', 'Cooler Master', '350W, basic efficiency rating.', 'Lab 4', 'SU23', 'Unequipped', 'Working', '2024-10-29 23:10:22'),
('P0017', 'PSU', 'Cooler Master', '350W, basic efficiency rating.', 'Lab 3', 'SU22', 'Unequipped', 'Working', '2024-10-29 23:10:22'),
('P0018', 'PSU', 'Cooler Master', '350W, basic efficiency rating.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:10:22'),
('P0019', 'PSU', 'Cooler Master', '350W, basic efficiency rating.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:10:22'),
('P0020', 'PSU', 'Cooler Master', '350W, basic efficiency rating.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:10:22'),
('R0011', 'RAM', 'ADATA', '4GB DDR4, 2400MHz.', 'Lab 4', 'SU23', 'Unequipped', 'Working', '2024-10-29 23:09:04'),
('R0012', 'RAM', 'ADATA', '4GB DDR4, 2400MHz.', 'Lab 3', 'SU22', 'Unequipped', 'Working', '2024-10-29 23:09:04'),
('R0013', 'RAM', 'ADATA', '4GB DDR4, 2400MHz.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:09:04'),
('R0014', 'RAM', 'ADATA', '4GB DDR4, 2400MHz.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:09:04'),
('R0015', 'RAM', 'ADATA', '4GB DDR4, 2400MHz.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:09:04'),
('S0016', 'Storage', 'Western Digital (WD)', '500GB HDD, 5400 RPM.', 'Lab 4', 'SU23', 'Unequipped', 'Working', '2024-10-29 23:11:53'),
('S0017', 'Storage', 'Western Digital (WD)', '500GB HDD, 5400 RPM.', 'Lab 3', 'SU22', 'Unequipped', 'Working', '2024-10-29 23:11:53'),
('S0018', 'Storage', 'Western Digital (WD)', '500GB HDD, 5400 RPM.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:11:53'),
('S0019', 'Storage', 'Western Digital (WD)', '500GB HDD, 5400 RPM.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:11:53'),
('SUC0020', 'System Unit Case', 'Ant Esports', 'Budget mid-tower case, minimal design, basic airflow.', 'Lab 3', 'SU22', 'Unequipped', 'Working', '2024-10-29 23:12:13'),
('SUC0021', 'System Unit Case', 'Ant Esports', 'Budget mid-tower case, minimal design, basic airflow.', 'Lab 4', 'SU23', 'Unequipped', 'Working', '2024-10-29 23:12:13'),
('SUC0022', 'System Unit Case', 'Ant Esports', 'Budget mid-tower case, minimal design, basic airflow.', 'Lab 3', 'Unequipped', 'Unequipped', 'Working', '2024-10-29 23:12:13');

-- --------------------------------------------------------

--
-- Table structure for table `asset_requests`
--

CREATE TABLE `asset_requests` (
  `request_id` int(11) NOT NULL,
  `room` varchar(20) NOT NULL,
  `instructor_id` varchar(20) NOT NULL,
  `asset_id` varchar(20) NOT NULL,
  `request_type` varchar(20) NOT NULL,
  `request_date` datetime DEFAULT NULL,
  `note` varchar(255) NOT NULL,
  `status` varchar(20) NOT NULL,
  `processed_date` datetime DEFAULT NULL,
  `processed_by` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `asset_requests`
--

INSERT INTO `asset_requests` (`request_id`, `room`, `instructor_id`, `asset_id`, `request_type`, `request_date`, `note`, `status`, `processed_date`, `processed_by`) VALUES
(21, 'Lab 2', 'U004', 'MB1014', 'Replacement', '2024-10-27 12:07:14', 'Need na po palitan sira na po kasi', 'Completed', '2024-10-27 15:13:30', 'U003'),
(22, 'Lab 3', 'U004', 'MO0006', 'Repair', '2024-10-30 10:31:20', 'Nagloloko po', 'Completed', '2024-11-16 09:52:14', 'U023'),
(23, 'Lab 3', 'U004', 'K0005', 'Repair', '2024-10-30 11:42:55', 'Some keys not working', 'Completed', '2024-11-16 09:57:15', 'U023'),
(24, 'Lab 3', 'U004', 'A0023', 'Repair', '2024-11-08 20:59:18', 'try', 'Completed', '2024-11-16 11:51:46', 'U023'),
(25, 'Lab 3', 'U004', 'A0023', 'Replacement', '2024-11-16 10:03:23', 'Ayaw po yung buttons', 'Completed', '2024-11-16 11:55:05', 'U023'),
(26, 'Lab 3', 'U004', 'A0024', 'Replacement', '2024-11-16 11:50:56', 'Needs replacement', 'Completed', '2024-11-16 11:51:56', 'U023');

-- --------------------------------------------------------

--
-- Table structure for table `attendance`
--

CREATE TABLE `attendance` (
  `attendance_id` int(11) NOT NULL,
  `workstation` varchar(20) NOT NULL,
  `student_id` varchar(50) NOT NULL,
  `schedule_id` int(11) NOT NULL,
  `time_in` time NOT NULL,
  `time_out` time NOT NULL,
  `duration` time NOT NULL,
  `attendance_date` date NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `attendance`
--

INSERT INTO `attendance` (`attendance_id`, `workstation`, `student_id`, `schedule_id`, `time_in`, `time_out`, `duration`, `attendance_date`) VALUES
(96, 'ACERPC', '21-UR-0001', 2, '13:50:18', '00:00:00', '00:00:00', '2024-11-24'),
(97, 'PC1', '21-UR-0001', 2, '18:48:11', '10:00:00', '08:00:00', '2024-11-25');

-- --------------------------------------------------------

--
-- Table structure for table `repair_history`
--

CREATE TABLE `repair_history` (
  `repair_history_id` int(11) NOT NULL,
  `request_id` int(11) NOT NULL,
  `system_unit_id` varchar(20) NOT NULL,
  `workstation_id` varchar(20) NOT NULL,
  `room` varchar(20) NOT NULL,
  `asset` varchar(20) NOT NULL,
  `requested_by` varchar(20) NOT NULL,
  `date_requested` datetime NOT NULL,
  `processed_by` varchar(20) NOT NULL,
  `date_processed` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `repair_history`
--

INSERT INTO `repair_history` (`repair_history_id`, `request_id`, `system_unit_id`, `workstation_id`, `room`, `asset`, `requested_by`, `date_requested`, `processed_by`, `date_processed`) VALUES
(1, 0, 'Unequipped', 'Unequipped', 'Lab 3', 'K0005', 'U004', '2024-10-30 11:42:55', 'U003', '2024-10-30 12:21:10'),
(2, 23, 'Unequipped', 'Unequipped', 'Lab 3', 'K0005', 'U004', '2024-10-30 11:42:55', 'U003', '2024-10-30 13:44:07'),
(3, 24, 'Unequipped', 'W002', 'Lab 3', 'A0023', 'U004', '2024-11-08 20:59:18', 'U003', '2024-11-08 21:22:13'),
(4, 24, 'Unequipped', 'W002', 'Lab 3', 'A0023', 'U004', '2024-11-08 20:59:18', 'U023', '2024-11-16 09:35:50'),
(5, 24, 'Unequipped', 'W002', 'Lab 3', 'A0023', 'U004', '2024-11-08 20:59:18', 'U023', '2024-11-16 09:42:50'),
(6, 22, 'Unequipped', 'W001', 'Lab 3', 'MO0006', 'U004', '2024-10-30 10:31:20', 'U023', '2024-11-16 09:52:14'),
(7, 23, 'Unequipped', 'Unequipped', 'Lab 3', 'K0005', 'U004', '2024-10-30 11:42:55', 'U023', '2024-11-16 09:57:15'),
(8, 24, 'Unequipped', 'Unequipped', 'Lab 3', 'A0023', 'U004', '2024-11-08 20:59:18', 'U023', '2024-11-16 11:51:46');

-- --------------------------------------------------------

--
-- Table structure for table `replacement_history`
--

CREATE TABLE `replacement_history` (
  `replacement_history_id` int(11) NOT NULL,
  `request_id` int(11) NOT NULL,
  `system_unit_id` varchar(10) NOT NULL,
  `workstation_id` varchar(10) NOT NULL,
  `room` varchar(20) NOT NULL,
  `current_asset` varchar(10) NOT NULL,
  `new_asset` varchar(10) NOT NULL,
  `requested_by` varchar(10) NOT NULL,
  `date_requested` datetime DEFAULT NULL,
  `processed_by` varchar(20) NOT NULL,
  `date_processed` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `replacement_history`
--

INSERT INTO `replacement_history` (`replacement_history_id`, `request_id`, `system_unit_id`, `workstation_id`, `room`, `current_asset`, `new_asset`, `requested_by`, `date_requested`, `processed_by`, `date_processed`) VALUES
(8, 0, 'SU22', 'W001', 'Lab 3', 'M0013', 'M0012', 'N/A', '2024-10-30 10:19:13', 'U003', '2024-10-30 10:19:13'),
(95, 25, 'Unequipped', 'W002', 'Lab 3', 'A0023', 'A0024', 'U004', '2024-11-16 10:03:23', 'U023', '2024-11-16 10:04:21'),
(267, 25, 'Unequipped', 'W002', 'Lab 3', 'A0023', 'A0024', 'U004', '2024-11-16 10:03:23', 'U023', '2024-11-16 11:55:05'),
(2000000, 26, 'Unequipped', 'W002', 'Lab 3', 'A0024', 'A0023', 'U004', '2024-11-16 11:50:56', 'U023', '2024-11-16 11:51:56');

-- --------------------------------------------------------

--
-- Table structure for table `rooms`
--

CREATE TABLE `rooms` (
  `id` int(11) NOT NULL,
  `laboratory_room` varchar(10) NOT NULL,
  `date_added` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `rooms`
--

INSERT INTO `rooms` (`id`, `laboratory_room`, `date_added`) VALUES
(1, 'Lab 1', '2024-09-10 22:17:55'),
(2, 'Lab 2', '2024-09-10 22:17:55'),
(3, 'Lab 3', '2024-09-10 22:17:55'),
(4, 'Lab 5', '2024-09-10 22:17:55'),
(7, 'Lab 9', '2024-09-12 18:47:47');

-- --------------------------------------------------------

--
-- Table structure for table `schedules`
--

CREATE TABLE `schedules` (
  `schedule_id` int(10) NOT NULL,
  `instructor` varchar(20) NOT NULL,
  `room` varchar(20) NOT NULL,
  `course` varchar(10) NOT NULL,
  `subject` varchar(50) NOT NULL,
  `year` varchar(5) NOT NULL,
  `block` varchar(5) NOT NULL,
  `day` varchar(10) NOT NULL,
  `class_start` time NOT NULL,
  `class_end` time NOT NULL,
  `date_added` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `schedules`
--

INSERT INTO `schedules` (`schedule_id`, `instructor`, `room`, `course`, `subject`, `year`, `block`, `day`, `class_start`, `class_end`, `date_added`) VALUES
(2, 'U004', 'Lab 3', 'BSIT', 'Fundamentals of Programming', '1', 'B', 'Monday', '00:00:00', '23:40:45', '2024-09-22 10:41:33');

-- --------------------------------------------------------

--
-- Table structure for table `students`
--

CREATE TABLE `students` (
  `student_id` varchar(10) NOT NULL,
  `password` varchar(100) NOT NULL,
  `firstname` varchar(50) NOT NULL,
  `middlename` varchar(50) NOT NULL,
  `lastname` varchar(50) NOT NULL,
  `course` varchar(10) NOT NULL,
  `year` int(11) NOT NULL,
  `block` varchar(10) NOT NULL,
  `birthdate` date DEFAULT NULL,
  `date_added` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `students`
--

INSERT INTO `students` (`student_id`, `password`, `firstname`, `middlename`, `lastname`, `course`, `year`, `block`, `birthdate`, `date_added`) VALUES
('21-UR-0001', 'newpass', 'Mark', 'Cruz', 'Garcia', 'BSIT', 1, 'C', NULL, '2024-11-04 02:16:47'),
('21-UR-0067', 'newpass', 'Stephen ', 'Cruz', 'Curry', 'BSIT', 1, 'C', NULL, '2024-11-25 21:50:59'),
('24-UR-0001', 'pass050', 'Lillian', 'Xenia', 'Cook', 'BSCE', 2, 'B', NULL, '2024-09-17 00:00:00'),
('24-UR-0002', 'pass036', 'Hannah', 'Jasmine', 'Perez', 'BSCE', 4, 'D', NULL, '2024-09-17 00:00:00'),
('24-UR-0003', 'newpass', 'Benjamin', 'Ian', 'Mitchell', 'BSIT', 1, 'B', NULL, '2024-09-17 00:00:00'),
('24-UR-0004', 'pass034', 'Lily', 'Holly', 'Carter', 'BSCE', 2, 'B', NULL, '2024-09-17 00:00:00'),
('24-UR-0005', 'pass033', 'Lucas', 'Gavin', 'Nelson', 'BSIT', 1, 'A', NULL, '2024-09-17 00:00:00'),
('24-UR-0006', 'pass032', 'Scarlett', 'Faith', 'Baker', 'BSCE', 4, 'D', NULL, '2024-09-17 00:00:00'),
('24-UR-0007', 'pass031', 'Henry', 'Evan', 'Adams', 'BSIT', 3, 'C', NULL, '2024-09-17 00:00:00'),
('24-UR-0008', 'pass030', 'Victoria', 'Diana', 'Green', 'BSCE', 2, 'B', NULL, '2024-09-17 00:00:00'),
('24-UR-0009', 'newpass', 'Jack', 'Caleb', 'Scott', 'BSIT', 1, 'A', NULL, '2024-09-17 00:00:00'),
('24-UR-0010', 'pass028', 'Grace', 'Brianna', 'Wright', 'BSCE', 4, 'D', NULL, '2024-09-17 00:00:00'),
('24-UR-0011', 'pass027', 'Sebastian', 'Aaron', 'King', 'BSIT', 3, 'C', NULL, '2024-09-17 00:00:00'),
('24-UR-0012', 'pass037', 'Samuel', 'Kyle', 'Roberts', 'BSIT', 1, 'A', NULL, '2024-09-17 00:00:00'),
('24-UR-0013', 'pass038', 'Zoe', 'Luna', 'Turner', 'BSCE', 2, 'B', NULL, '2024-09-17 00:00:00'),
('24-UR-0014', 'pass039', 'Ethan', 'Mason', 'Phillips', 'BSIT', 3, 'C', NULL, '2024-09-17 00:00:00'),
('24-UR-0015', 'pass049', 'Owen', 'Wyatt', 'Reed', 'BSIT', 1, 'A', NULL, '2024-09-17 00:00:00'),
('24-UR-0016', 'pass048', 'Chloe', 'Vera', 'Rogers', 'BSCE', 4, 'D', NULL, '2024-09-17 00:00:00'),
('24-UR-0017', 'pass047', 'Logan', 'Umar', 'Morris', 'BSIT', 3, 'C', NULL, '2024-09-17 00:00:00'),
('24-UR-0018', 'pass046', 'Mila', 'Tessa', 'Sanchez', 'BSCE', 2, 'B', NULL, '2024-09-17 00:00:00'),
('24-UR-0019', 'pass045', 'Isaac', 'Shane', 'Stewart', 'BSIT', 1, 'A', NULL, '2024-09-17 00:00:00'),
('24-UR-0020', 'pass044', 'Aria', 'Riley', 'Collins', 'BSCE', 4, 'D', NULL, '2024-09-17 00:00:00'),
('24-UR-0021', 'pass043', 'Nathan', 'Quinn', 'Edwards', 'BSIT', 3, 'C', NULL, '2024-09-17 00:00:00'),
('24-UR-0022', 'pass042', 'Layla', 'Peyton', 'Evans', 'BSCE', 2, 'B', NULL, '2024-09-17 00:00:00'),
('24-UR-0023', 'pass041', 'Dylan', 'Oscar', 'Parker', 'BSIT', 1, 'A', NULL, '2024-09-17 00:00:00'),
('24-UR-0024', 'pass040', 'Avery', 'Nina', 'Campbell', 'BSCE', 4, 'D', NULL, '2024-09-17 00:00:00'),
('24-UR-0025', 'pass026', 'Ella', 'Zara', 'Allen', 'BSCE', 2, 'B', NULL, '2024-09-17 00:00:00'),
('24-UR-0026', 'pass025', 'Matthew', 'Yosef', 'Hall', 'BSIT', 1, 'A', NULL, '2024-09-17 00:00:00'),
('24-UR-0027', 'pass011', 'Noah', 'Kevin', 'Gonzalez', 'BSIT', 3, 'C', NULL, '2024-09-17 00:00:00'),
('24-UR-0028', 'pass010', 'Ava', 'Jessica', 'Lopez', 'BSCE', 2, 'B', NULL, '2024-09-17 00:00:00'),
('24-UR-0029', 'pass009', 'Liam', 'Isaac', 'Davis', 'BSIT', 1, 'A', NULL, '2024-09-17 00:00:00'),
('24-UR-0030', 'pass008', 'Olivia', 'Hannah', 'Martinez', 'BSCE', 4, 'D', NULL, '2024-09-17 00:00:00'),
('24-UR-0031', 'pass007', 'James', 'George', 'Miller', 'BSIT', 3, 'C', NULL, '2024-09-17 00:00:00'),
('24-UR-0032', 'pass006', 'Sophia', 'Fiona', 'Garcia', 'BSCE', 1, 'B', NULL, '2024-09-17 00:00:00'),
('24-UR-0033', 'pass005', 'David', 'Edward', 'Jones', 'BSIT', 2, 'A', NULL, '2024-09-17 00:00:00'),
('24-UR-0034', 'pass004', 'Sarah', 'Danielle', 'Brown', 'BSCE', 4, 'D', NULL, '2024-09-17 00:00:00'),
('24-UR-0035', 'pass003', 'Michael', 'Charles', 'Williams', 'BSIT', 3, 'C', NULL, '2024-09-17 00:00:00'),
('24-UR-0036', 'pass002', 'Emily', 'Beatrice', 'Johnson', 'BSCE', 1, 'B', NULL, '2024-09-17 00:00:00'),
('24-UR-0037', 'pass013', 'Mason', 'Matthew', 'Anderson', 'BSIT', 1, 'A', NULL, '2024-09-17 00:00:00'),
('24-UR-0038', 'pass012', 'Isabella', 'Lillian', 'Wilson', 'BSCE', 4, 'D', NULL, '2024-09-17 00:00:00'),
('24-UR-0039', 'pass020', 'Abigail', 'Tara', 'Harris', 'BSCE', 4, 'D', NULL, '2024-09-17 00:00:00'),
('24-UR-0040', 'pass023', 'Daniel', 'William', 'Walker', 'BSIT', 3, 'C', NULL, '2024-09-17 00:00:00'),
('24-UR-0041', 'pass022', 'Sofia', 'Victoria', 'Lewis', 'BSCE', 2, 'B', NULL, '2024-09-17 00:00:00'),
('24-UR-0042', 'pass021', 'Alexander', 'Ulysses', 'Clark', 'BSIT', 1, 'A', NULL, '2024-09-17 00:00:00'),
('24-UR-0043', 'pass019', 'Elijah', 'Samuel', 'White', 'BSIT', 3, 'C', NULL, '2024-09-17 00:00:00'),
('24-UR-0044', 'pass018', 'Charlotte', 'Rose', 'Jackson', 'BSCE', 2, 'B', NULL, '2024-09-17 00:00:00'),
('24-UR-0045', 'pass017', 'William', 'Quentin', 'Martin', 'BSIT', 1, 'A', NULL, '2024-09-17 00:00:00'),
('24-UR-0046', 'pass016', 'Amelia', 'Paige', 'Moore', 'BSCE', 4, 'D', NULL, '2024-09-17 00:00:00'),
('24-UR-0047', 'pass015', 'Jacob', 'Oliver', 'Taylor', 'BSIT', 3, 'C', NULL, '2024-09-17 00:00:00'),
('24-UR-0048', 'pass014', 'Mia', 'Natalie', 'Thomas', 'BSCE', 2, 'B', NULL, '2024-09-17 00:00:00'),
('24-UR-0049', 'pass024', 'Harper', 'Ximena', 'Young', 'BSCE', 4, 'D', NULL, '2024-09-17 00:00:00'),
('24-UR-0050', 'temp123', 'Juan', 'Dela', 'Curz', 'BSIT', 1, 'C', NULL, '2024-09-20 17:47:03'),
('24-UR-0051', 'temp123', 'Stephen', 'Johnson', 'Curry', 'BSCOE', 3, 'B', NULL, '2024-10-06 01:54:04'),
('24-UR-0052', 'temp123', 'Mark', 'Joseph', 'Herra', 'BSCOE', 2, 'E', NULL, '2024-10-06 21:22:10'),
('24-UR-0053', 'temp123', 'asd', 'sdf', 'sdf', 'BSIT', 1, 'A', NULL, '2024-10-08 20:12:48'),
('24-UR-0054', '11042024', 'Juan', 'Dela', 'Cruz', 'BSIT', 1, 'A', '2024-11-04', '2024-11-04 00:40:30'),
('24-UR-0055', '11042024', 'Juan ', 'Dela', 'Cruz', 'BSIT', 1, 'A', '2024-11-04', '2024-11-04 00:43:22');

-- --------------------------------------------------------

--
-- Table structure for table `system_units`
--

CREATE TABLE `system_units` (
  `system_unit_id` varchar(10) NOT NULL,
  `hostname` varchar(50) NOT NULL,
  `operating_system` varchar(20) NOT NULL,
  `motherboard` varchar(50) NOT NULL,
  `cpu` varchar(50) NOT NULL,
  `ram` varchar(50) NOT NULL,
  `gpu` varchar(50) NOT NULL,
  `psu` varchar(50) NOT NULL,
  `system_unit_case` varchar(50) NOT NULL,
  `storage` varchar(20) NOT NULL,
  `location` varchar(20) NOT NULL DEFAULT 'Unassigned',
  `workstation` varchar(10) NOT NULL DEFAULT 'Unequipped',
  `added_by` varchar(20) NOT NULL,
  `date_built` datetime NOT NULL DEFAULT current_timestamp(),
  `last_updated` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `system_units`
--

INSERT INTO `system_units` (`system_unit_id`, `hostname`, `operating_system`, `motherboard`, `cpu`, `ram`, `gpu`, `psu`, `system_unit_case`, `storage`, `location`, `workstation`, `added_by`, `date_built`, `last_updated`) VALUES
('SU22', 'pc1', 'Windows', 'MB0012', 'C0017', 'R0012', 'G0024', 'P0017', 'SUC0020', 'S0017', 'Lab 3', 'W001', 'U003', '2024-10-29 23:19:48', NULL),
('SU23', 'pc1', 'Windows', 'MB0011', 'C0018', 'R0011', 'G0023', 'P0016', 'SUC0021', 'S0016', 'Lab 3', 'W002', 'U003', '2024-10-29 23:48:11', '2024-10-30 17:43:08'),
('SU24', 'pc6', 'MAC', 'N/A', 'N/A', 'N/A', 'N/A', 'N/A', 'N/A', 'N/A', 'Lab 3', 'Unequipped', 'U003', '2024-11-24 17:59:55', NULL),
('SU25', 'pc10', 'Linux', 'MB0011', 'C0018', 'R0011', 'G0023', 'P0016', 'SUC0021', 'S0016', 'Lab 3', 'Unequipped', 'U003', '2024-10-29 23:48:11', '2024-10-30 17:43:08');

-- --------------------------------------------------------

--
-- Table structure for table `temporary_schedules`
--

CREATE TABLE `temporary_schedules` (
  `schedule_id` int(11) NOT NULL,
  `instructor` varchar(20) NOT NULL,
  `room` varchar(20) NOT NULL,
  `course` varchar(20) NOT NULL,
  `subject` varchar(150) NOT NULL,
  `year` varchar(10) NOT NULL,
  `block` varchar(20) NOT NULL,
  `day` varchar(20) NOT NULL,
  `class_start` time NOT NULL,
  `class_end` time NOT NULL,
  `status` varchar(20) NOT NULL DEFAULT 'Pending',
  `date_added` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` varchar(10) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(100) NOT NULL DEFAULT 'temp123',
  `firstname` varchar(50) NOT NULL,
  `middlename` varchar(50) NOT NULL,
  `lastname` varchar(50) NOT NULL,
  `role` varchar(20) NOT NULL,
  `assigned_room` varchar(10) NOT NULL DEFAULT 'Unassigned',
  `birthdate` date DEFAULT NULL,
  `date_added` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `username`, `password`, `firstname`, `middlename`, `lastname`, `role`, `assigned_room`, `birthdate`, `date_added`) VALUES
('U001', 'admin001', 'adminpass', 'Brylle', 'Dela', 'Cruz', 'Admin', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U002', 'admin002', 'password2', 'Maria', 'Santos', 'Ramos', 'Instructor', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U003', 'labincharge001', 'password3', 'Pedro', 'Bautista', 'Flores', 'Incharge', 'Lab 3', NULL, '2024-09-27 03:09:35'),
('U004', 'labincharge002', 'password4', 'Jose', 'Luna', 'Aquino', 'Instructor', 'Lab 2', NULL, '2024-09-27 03:09:35'),
('U005', 'instructor001', 'password5', 'Ana', 'Reyes', 'Castro', 'Incharge', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U006', 'instructor002', 'password6', 'Carlos', 'Morales', 'Dizon', 'Admin', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U012', 'instructor004', 'password12', 'Roberto', 'De', 'La Cruz', 'Incharge', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U013', 'admin005', 'password13', 'Isabel', 'Ponce', 'Santos', 'Admin', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U014', 'admin006', 'password14', 'Henry', 'De', 'Jesus', 'Admin', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U015', 'labincharge005', 'password15', 'Angel', 'Garcia', 'Yap', 'Incharge', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U016', 'labincharge006', 'password16', 'Julia', 'Dela', 'Torre', 'Incharge', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U017', 'instructor005', 'password17', 'Victor', 'Ang', 'Martinez', 'Admin', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U018', 'instructor006', 'password18', 'Sophia', 'Tan', 'López', 'Incharge', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U019', 'admin007', 'password19', 'Edward', 'Lim', 'Hernandez', 'Incharge', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U023', 'instructor007', 'password23', 'Carlos', 'Villanueva', 'Santiago', 'Instructor/Incharge', 'Lab 3', NULL, '2024-09-27 03:09:35'),
('U024', 'instructor008', 'password24', 'Emma', 'Alvarez', 'Marquez', 'Incharge', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U026', 'admin010', 'password26', 'Mariano', 'Aguilar', 'Velasco', 'Instructor', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U027', 'labincharge009', 'password27', 'Nina', 'Alonzo', 'Ramos', 'Incharge', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U030', 'instructor010', 'password30', 'Leo', 'López', 'Reyes', 'Admin', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U033', 'labincharge011', 'password33', 'Mia', 'Rojas', 'Ferrer', 'Instructor/Incharge', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U034', 'labincharge012', 'password34', 'Robert', 'Tiongson', 'Carreon', 'Instructor', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U035', 'instructor011', 'password35', 'Tina', 'Alvarado', 'Ramirez', 'Instructor/Incharge', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U036', 'instructor012', 'password36', 'Martin', 'De', 'La Cruz', 'Admin', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U037', 'admin013', 'password37', 'Patricia', 'Tan', 'Dela Cruz', 'Instructor/Incharge', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U038', 'admin014', 'password38', 'John', 'Pangilinan', 'Bautista', 'Incharge', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U039', 'labincharge013', 'password39', 'Rico', 'Santiago', 'Zamora', 'Admin', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U040', 'labincharge014', 'password40', 'Cynthia', 'Robles', 'Puno', 'Incharge', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U041', 'instructor013', 'password41', 'Jerome', 'Lim', 'Belen', 'Instructor', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U042', 'instructor014', 'password42', 'Leila', 'Ramirez', 'Fuentes', 'Incharge', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U043', 'admin015', 'password43', 'Marlon', 'Bautista', 'Cruz', 'Instructor', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U044', 'admin016', 'password44', 'Cecilia', 'Gonzales', 'Ocampo', 'Admin', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U045', 'labincharge015', 'password45', 'Henry', 'Rafael', 'Galvez', 'Incharge', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U046', 'labincharge016', 'password46', 'Daisy', 'Arroyo', 'Mansilungan', 'Instructor', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U047', 'instructor015', 'password47', 'Nico', 'Dela', 'Cruz', 'Instructor', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U048', 'instructor016', 'password48', 'Ayla', 'De', 'Jesus', 'Instructor', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U049', 'admin017', 'password49', 'Jenna', 'Buan', 'Tomas', 'Admin', 'Unassigned', NULL, '2024-09-27 03:09:35'),
('U052', 'markie', 'temp123', 'Mark', 'Dela Cruz', 'Celeste', 'Incharge', 'Lab 4', NULL, '2024-10-08 00:00:00'),
('U053', 'mark.me', 'temp123', 'Mark', 'Hello', 'Meow', 'Instructor', 'Unassigned', NULL, '2024-10-08 17:31:16'),
('U055', 'mgarcia', '10151990', 'Mark', 'Cruz', 'Garcia', 'Instructor', 'Unassigned', '1990-10-15', '2024-11-04 11:05:17');

-- --------------------------------------------------------

--
-- Table structure for table `workstations`
--

CREATE TABLE `workstations` (
  `workstation_id` varchar(10) NOT NULL,
  `system_unit` varchar(10) NOT NULL,
  `room` varchar(10) NOT NULL,
  `monitor` varchar(10) NOT NULL,
  `keyboard` varchar(10) NOT NULL,
  `mouse` varchar(10) NOT NULL,
  `avr` varchar(10) NOT NULL,
  `time_usage` bigint(20) NOT NULL,
  `added_by` varchar(20) NOT NULL,
  `date_added` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `workstations`
--

INSERT INTO `workstations` (`workstation_id`, `system_unit`, `room`, `monitor`, `keyboard`, `mouse`, `avr`, `time_usage`, `added_by`, `date_added`) VALUES
('W001', 'SU22', 'Lab 3', 'M0013', 'K0002', 'MO0006', 'A0024', 82, 'U003', '2024-10-29 23:50:11'),
('W002', 'SU24', 'Lab 3', 'M0013', 'K0002', 'MO0006', 'A0024', 82, 'U003', '2024-10-29 23:50:11');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `assets`
--
ALTER TABLE `assets`
  ADD PRIMARY KEY (`asset_id`);

--
-- Indexes for table `asset_requests`
--
ALTER TABLE `asset_requests`
  ADD PRIMARY KEY (`request_id`);

--
-- Indexes for table `attendance`
--
ALTER TABLE `attendance`
  ADD PRIMARY KEY (`attendance_id`);

--
-- Indexes for table `repair_history`
--
ALTER TABLE `repair_history`
  ADD PRIMARY KEY (`repair_history_id`);

--
-- Indexes for table `replacement_history`
--
ALTER TABLE `replacement_history`
  ADD PRIMARY KEY (`replacement_history_id`);

--
-- Indexes for table `rooms`
--
ALTER TABLE `rooms`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `schedules`
--
ALTER TABLE `schedules`
  ADD PRIMARY KEY (`schedule_id`);

--
-- Indexes for table `students`
--
ALTER TABLE `students`
  ADD PRIMARY KEY (`student_id`);

--
-- Indexes for table `system_units`
--
ALTER TABLE `system_units`
  ADD PRIMARY KEY (`system_unit_id`);

--
-- Indexes for table `temporary_schedules`
--
ALTER TABLE `temporary_schedules`
  ADD PRIMARY KEY (`schedule_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`);

--
-- Indexes for table `workstations`
--
ALTER TABLE `workstations`
  ADD PRIMARY KEY (`workstation_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `asset_requests`
--
ALTER TABLE `asset_requests`
  MODIFY `request_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT for table `attendance`
--
ALTER TABLE `attendance`
  MODIFY `attendance_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=98;

--
-- AUTO_INCREMENT for table `repair_history`
--
ALTER TABLE `repair_history`
  MODIFY `repair_history_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `replacement_history`
--
ALTER TABLE `replacement_history`
  MODIFY `replacement_history_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2000001;

--
-- AUTO_INCREMENT for table `rooms`
--
ALTER TABLE `rooms`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `schedules`
--
ALTER TABLE `schedules`
  MODIFY `schedule_id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `temporary_schedules`
--
ALTER TABLE `temporary_schedules`
  MODIFY `schedule_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
