<?php
session_start();
include 'conn.php';

// Check if the student is logged in
if (!isset($_SESSION['student_id'])) {
    echo json_encode(['success' => false, 'message' => 'You are not logged in.']);
    exit();
}

// Get student ID from session
$student_id = $_SESSION['student_id'];

// Check if the request is a POST request
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    // Retrieve current and new passwords from the POST request
    $current_password = $_POST['current_password'];
    $new_password = $_POST['new_password'];
    $confirm_password = $_POST['confirm_password'];

    // Check if new password matches confirmation
    if ($new_password !== $confirm_password) {
        echo json_encode(['success' => false, 'message' => 'New passwords do not match.']);
        exit();
    }

    // Retrieve the student's current password from the database
    $query = "SELECT password FROM students WHERE student_id = ?";
    $stmt = $conn->prepare($query);
    $stmt->bind_param("s", $student_id);
    $stmt->execute();
    $result = $stmt->get_result();

    if ($result->num_rows === 0) {
        echo json_encode(['success' => false, 'message' => 'Student not found.']);
        exit();
    }

    $student = $result->fetch_assoc();

    // Verify the current password
    if ($current_password !== $student['password']) { // Here you can add password hashing verification
        echo json_encode(['success' => false, 'message' => 'Current password is incorrect.']);
        exit();
    }

    // Update the student's password in the database
    $updateQuery = "UPDATE students SET password = ? WHERE student_id = ?";
    $stmt = $conn->prepare($updateQuery);
    $stmt->bind_param("ss", $new_password, $student_id);
    if ($stmt->execute()) {
        echo json_encode(['success' => true, 'message' => 'Password changed successfully.']);
    } else {
        echo json_encode(['success' => false, 'message' => 'Error updating password.']);
    }
} else {
    echo json_encode(['success' => false, 'message' => 'Invalid request method.']);
}
?>
