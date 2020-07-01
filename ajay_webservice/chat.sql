-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 08, 2017 at 07:08 AM
-- Server version: 10.1.21-MariaDB
-- PHP Version: 5.6.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `madmuc`
--

-- --------------------------------------------------------

--
-- Table structure for table `chat`
--

CREATE TABLE `chat` (
  `id` int(11) NOT NULL,
  `user` varchar(32) NOT NULL,
  `message` varchar(1024) NOT NULL,
  `room` int(11) NOT NULL,
  `postdate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `chat`
--

INSERT INTO `chat` (`id`, `user`, `message`, `room`, `postdate`) VALUES
(9, 'Kiron', 'Now it\'s okay.', 1, '2017-05-08 03:52:51'),
(10, 'Lion', 'Roar!', 1, '2017-05-08 03:52:51'),
(11, 'Macro', 'Ole', 1, '2017-05-08 03:54:11'),
(12, 'Juliet', 'Romeo', 1, '2017-05-08 03:54:11'),
(13, 'Delta', 'Hello World', 1, '2017-05-08 03:54:36'),
(14, 'Alpha', 'Beta & Gamma', 1, '2017-05-08 03:54:36'),
(15, 'Localhost', '404 page not found', 1, '2017-05-08 03:55:12'),
(16, 'Globalhost', 'Internet not found.', 1, '2017-05-08 03:55:12'),
(17, 'Dragon Queen', 'Gimme the $5 meal.', 1, '2017-05-08 03:55:47'),
(18, 'Horse', 'Eighhawwww!', 1, '2017-05-08 03:55:47'),
(19, 'Marco', 'Polo', 1, '2017-05-08 03:56:43');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `chat`
--
ALTER TABLE `chat`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `chat`
--
ALTER TABLE `chat`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
