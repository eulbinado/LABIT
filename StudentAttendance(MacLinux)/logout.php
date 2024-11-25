<?php
session_start();
include 'conn.php';

if (isset($_POST['logout'])) {
    $student_id = $_SESSION['student_id'];
    logOutSession($conn, $student_id);
}
?>
