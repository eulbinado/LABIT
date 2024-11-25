<?php
session_start();
include 'conn.php';

// Enable error reporting
error_reporting(E_ALL);
ini_set('display_errors', 1);

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $student_id = $_POST['student_id'];
    $password = $_POST['password'];

    function authenticateStudent($conn, $student_id, $password)
    {
        $query = "SELECT COUNT(*) AS count FROM students WHERE student_id = ? AND password = ?";
        $stmt = $conn->prepare($query);
        $stmt->bind_param("ss", $student_id, $password);
        $stmt->execute();
        $result = $stmt->get_result();
        $row = $result->fetch_assoc();
        return $row['count'] > 0;
    }

    function checkSchedule($conn, $student_id)
    {
        $query = "SELECT year, block FROM students WHERE student_id = ?";
        $stmt = $conn->prepare($query);
        $stmt->bind_param("s", $student_id);
        $stmt->execute();
        $studentResult = $stmt->get_result();

        if ($studentResult->num_rows > 0) {
            $studentData = $studentResult->fetch_assoc();
            $year = $studentData['year'];
            $block = $studentData['block'];
            $currentTime = date("H:i:s");

            // Check in temporary schedules
            $tempScheduleQuery = "SELECT schedule_id, instructor, subject, class_start, class_end, room FROM temporary_schedules WHERE year = ? AND block = ? AND status = 'Approved' AND ? BETWEEN class_start AND class_end";
            $tempStmt = $conn->prepare($tempScheduleQuery);
            $tempStmt->bind_param("sss", $year, $block, $currentTime);
            $tempStmt->execute();
            $tempScheduleResult = $tempStmt->get_result();

            if ($tempScheduleResult->num_rows > 0) {
                return processScheduleData($conn, $student_id, $tempScheduleResult);
            }

            // Check in regular schedules
            $scheduleQuery = "SELECT schedule_id, instructor, subject, class_start, class_end, room FROM schedules WHERE year = ? AND block = ? AND ? BETWEEN class_start AND class_end";
            $scheduleStmt = $conn->prepare($scheduleQuery);
            $scheduleStmt->bind_param("sss", $year, $block, $currentTime);
            $scheduleStmt->execute();
            $scheduleResult = $scheduleStmt->get_result();

            if ($scheduleResult->num_rows > 0) {
                return processScheduleData($conn, $student_id, $scheduleResult);
            } else {
                return ["success" => false, "message" => "No class is scheduled for this time."];
            }
        } else {
            return ["success" => false, "message" => "Student record not found."];
        }
    }

    function processScheduleData($conn, $student_id, $scheduleData)
    {
        $schedule = $scheduleData->fetch_assoc();
        $scheduleID = $schedule['schedule_id'];

        if (hasAlreadyMarkedAttendance($conn, $student_id, $scheduleID)) {
            return ["success" => false, "message" => "You have already marked attendance for this class today."];
        }

        if (markAttendance($conn, $student_id, $scheduleID)) {
            // Return schedule details including room and subject for display on student-session.php
            return [
                "success" => true,
                "message" => "Attendance marked successfully.",
                "room" => $schedule['room'],
                "subject" => $schedule['subject']
            ];
        } else {
            return ["success" => false, "message" => "Error marking attendance."];
        }
    }

    function hasAlreadyMarkedAttendance($conn, $student_id, $scheduleID)
    {
        $currentDate = date("Y-m-d");
        $checkAttendanceQuery = "SELECT COUNT(*) AS count FROM attendance WHERE student_id = ? AND schedule_id = ? AND attendance_date = ?";
        $stmt = $conn->prepare($checkAttendanceQuery);
        $stmt->bind_param("sis", $student_id, $scheduleID, $currentDate);
        $stmt->execute();
        $result = $stmt->get_result();
        $row = $result->fetch_assoc();
        return $row['count'] > 0;
    }

    function markAttendance($conn, $student_id, $schedule_id)
    {
        date_default_timezone_set('Asia/Manila');

        $currentDate = date("Y-m-d");
        $currentTime = date("H:i:s");
        $hostname = gethostname();

        $insertAttendanceQuery = "INSERT INTO attendance (student_id, schedule_id, attendance_date, time_in, workstation) VALUES (?, ?, ?, ?, ?)";
        $stmt = $conn->prepare($insertAttendanceQuery);
        $stmt->bind_param("sssss", $student_id, $schedule_id, $currentDate, $currentTime, $hostname);
        return $stmt->execute();
    }

    if (authenticateStudent($conn, $student_id, $password)) {
        $_SESSION['student_id'] = $student_id;
        $scheduleCheck = checkSchedule($conn, $student_id);

        if ($scheduleCheck['success']) {
            // Store room and subject in session for use in student-session.php
            $_SESSION['room'] = $scheduleCheck['room'];
            $_SESSION['subject'] = $scheduleCheck['subject'];
        }

        echo json_encode($scheduleCheck);
    } else {
        echo json_encode(["success" => false, "message" => "Invalid student ID or password."]);
    }

    $conn->close();
    exit;
}
?>




<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Student Login</title>
    <link rel="icon" href="images/final-logo.png" type="image/x-icon">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500&display=swap" rel="stylesheet">
    <style>
        body {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            background-color: #f8f9fa;
            font-family: 'Montserrat', sans-serif;
            position: relative;
        }

        .login-container {
            max-width: 400px;
            width: 100%;
            padding: 2rem;
            border-radius: 8px;
            background-color: #ffffff;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
        }

        .login-header {
            font-weight: 500;
            font-size: 1.3rem;
            margin-bottom: 1.5rem;
            text-align: center;
        }

        .login-image {
            display: block;
            margin: 0 auto .7rem;
            max-width: 100%;
            height: auto;
        }

        #alertContainer {
            position: absolute;
            top: 20px;
            left: 50%;
            transform: translateX(-50%);
            z-index: 1050;
            width: 100%;
            max-width: 400px;
        }
    </style>
</head>

<body>

    <div class="login-container">
        <img src="images/final-logo.png" alt="School Logo" class="login-image">
        <div class="login-header">Login Your Student Account</div>
        <div class="container mt-3">
            <div id="alertContainer" aria-live="polite"></div>

            <form id="loginForm">
                <div class="mb-3">
                    <label for="studentId" class="form-label">Student ID</label>
                    <input type="text" class="form-control" id="studentId" name="student_id" placeholder="Enter your student ID" required>
                </div>
                <div class="mb-3">
                    <label for="password" class="form-label">Password</label>
                    <input type="password" class="form-control" id="password" name="password" placeholder="Enter your password" required>
                </div>
                <div class="mb-3 form-check">
                    <input type="checkbox" class="form-check-input" id="showPassword" onclick="togglePasswordVisibility()">
                    <label class="form-check-label" for="showPassword">Show Password</label>
                </div>
                <button type="submit" class="btn btn-primary w-100">Login</button>
            </form>
        </div>
    </div>

    <!-- Load jQuery first -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>

    <script>
        function togglePasswordVisibility() {
            const passwordInput = document.getElementById('password');
            const showPasswordCheckbox = document.getElementById('showPassword');
            passwordInput.type = showPasswordCheckbox.checked ? 'text' : 'password';
        }

        $(document).ready(function() {
            $('#loginForm').submit(function(event) {
                event.preventDefault(); // Prevent the default form submission

                // Get the form data
                const formData = $(this).serialize();

                // Make the AJAX request
                $.ajax({
                    url: 'index.php', // Specify the URL explicitly
                    type: 'POST',
                    data: formData,
                    dataType: 'json',
                    success: function(response) {
                        if (response.success) {
                            window.location.href = 'student-session.php';
                        } else {
                            $('#alertContainer').html('<div class="alert alert-danger" role="alert">' + response.message + '</div>');
                            setTimeout(function() {
                                $('#alertContainer').fadeOut('slow', function() {
                                    $(this).empty().show();
                                });
                            }, 3000);
                        }
                    },
                    error: function(jqXHR, textStatus, errorThrown) {
                        console.error("Error status: " + textStatus);
                        console.error("Error thrown: " + errorThrown);
                        console.error("Response text: " + jqXHR.responseText);
                        $('#alertContainer').html('<div class="alert alert-danger" role="alert">An error occurred. Please try again later.</div>');
                        setTimeout(function() {
                            $('#alertContainer').fadeOut('slow', function() {
                                $(this).empty().show();
                            });
                        }, 3000);
                    }
                });
            });
        });
    </script>

</body>

</html>