<?php
session_start();
include 'conn.php';

date_default_timezone_set('Asia/Manila');

if (!isset($_SESSION['student_id'])) {
    header("Location: index.php");
    exit();
}

$room = isset($_SESSION['room']) ? $_SESSION['room'] : 'Unknown';
$subject = isset($_SESSION['subject']) ? $_SESSION['subject'] : 'Unknown';

$student_id = $_SESSION['student_id'];
$query = "SELECT firstname, lastname FROM students WHERE student_id = ?";
$stmt = $conn->prepare($query);
$stmt->bind_param("s", $student_id);
$stmt->execute();
$result = $stmt->get_result();
$student = $result->fetch_assoc();

function logOutSession($conn, $student_id)
{
    $attendanceQuery = "SELECT attendance_id, time_in FROM attendance WHERE student_id = ? ORDER BY time_in DESC LIMIT 1";
    $stmt = $conn->prepare($attendanceQuery);
    $stmt->bind_param("s", $student_id);
    $stmt->execute();
    $attendanceResult = $stmt->get_result();

    if ($attendanceResult->num_rows > 0) {
        $attendanceData = $attendanceResult->fetch_assoc();
        $timeIn = new DateTime($attendanceData['time_in']);
        $timeOut = new DateTime();
        $duration = $timeOut->diff($timeIn);

        $updateQuery = "UPDATE attendance SET time_out = ?, duration = ? WHERE attendance_id = ?";
        $stmt = $conn->prepare($updateQuery);
        $timeOutFormatted = $timeOut->format('H:i:s');
        $durationFormatted = $duration->format('%H:%i:%s');  
        $stmt->bind_param("ssi", $timeOutFormatted, $durationFormatted, $attendanceData['attendance_id']);
        $stmt->execute();

        $hostname = gethostname();  
        $workstationQuery = "SELECT workstation FROM system_units WHERE hostname = ?";
        $stmt = $conn->prepare($workstationQuery);
        $stmt->bind_param("s", $hostname);
        $stmt->execute();
        $workstationResult = $stmt->get_result();

        if ($workstationResult->num_rows > 0) {
            $workstationData = $workstationResult->fetch_assoc();
            $workstationID = $workstationData['workstation'];

            $updateTimeUsageQuery = "UPDATE workstations SET time_usage = time_usage + ? WHERE workstation_id = ?";
            $stmt = $conn->prepare($updateTimeUsageQuery);
            $durationInSeconds = $duration->h * 3600 + $duration->i * 60 + $duration->s; 
            $stmt->bind_param("is", $durationInSeconds, $workstationID);
            $stmt->execute();
        } else {
            echo "System unit with the specified hostname was not found.";
        }
    } else {
        echo "No active session found.";
    }

    session_destroy();
    header("Location: index.php");
    exit();
}

if ($_SERVER['REQUEST_METHOD'] === 'POST' && isset($_POST['logout'])) {
    logOutSession($conn, $student_id);
}
?>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Student Session</title>
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
        }

        .session-container {
            max-width: 500px;
            width: 100%;
            padding: 2rem;
            border-radius: 8px;
            background-color: #ffffff;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            text-align: center;
        }

        .session-header {
            font-weight: 500;
            font-size: 1.5rem;
            margin-bottom: 1rem;
        }

        .btn-custom {
            font-weight: 500;
            width: 100%;
            margin-top: 10px;
        }

        #alertContainer {
            position: absolute;
            top: 20px;
            left: 50%;
            transform: translateX(-50%);
            z-index: 1100;
            width: 100%;
            max-width: 400px;
            text-align: left;
        }

        .modal-dialog {
            margin-top: 100px;
        }

        .session-info {
            font-weight: 500;
            font-size: 1.1rem;
            color: #6c757d;
            margin-bottom: 1rem;
        }

        .room,
        .subject {
            font-weight: 600;
            color: #495057;
        }
    </style>
</head>

<body>
    <div class="session-container">
        <div id="alertContainer" aria-live="polite"></div>

        <div class="session-header">Welcome, <?php echo htmlspecialchars($student['firstname'] . ' ' . $student['lastname']); ?>!</div>
        <p class="text-muted">You are currently logged into your class.</p>
        <p class="session-info">
            <span class="room"><?php echo htmlspecialchars($room); ?></span> |
            <span class="subject"><?php echo htmlspecialchars($subject); ?></span>
        </p>

        <button class="btn btn-outline-primary btn-custom" data-bs-toggle="modal" data-bs-target="#changePasswordModal">Change Password</button>

        <form action="" method="post" style="margin-top: 10px;">
            <input type="hidden" name="logout" value="true">
            <button type="submit" class="btn btn-danger btn-custom">Logout</button>
        </form>
    </div>

    <div class="modal fade" id="changePasswordModal" tabindex="-1" aria-labelledby="changePasswordLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="changePasswordLabel">Change Password</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form id="changePasswordForm">
                        <div class="mb-3">
                            <label for="currentPassword" class="form-label">Current Password</label>
                            <input type="password" class="form-control" id="currentPassword" name="current_password" required>
                        </div>
                        <div class="mb-3">
                            <label for="newPassword" class="form-label">New Password</label>
                            <input type="password" class="form-control" id="newPassword" name="new_password" required>
                        </div>
                        <div class="mb-3">
                            <label for="confirmPassword" class="form-label">Confirm New Password</label>
                            <input type="password" class="form-control" id="confirmPassword" name="confirm_password" required>
                        </div>
                        <div class="mb-3 form-check">
                            <input type="checkbox" class="form-check-input" id="showPassword">
                            <label class="form-check-label" for="showPassword">Show Password</label>
                        </div>
                        <button type="submit" class="btn btn-primary w-100">Update Password</button>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>

    <script>
        $(document).ready(function() {
            $('#changePasswordForm').submit(function(event) {
                event.preventDefault();

                $.ajax({
                    url: 'change-password.php',
                    type: 'POST',
                    data: $(this).serialize(),
                    dataType: 'json',
                    success: function(response) {

                        $('#alertContainer').empty();

                        const alertClass = response.success ? 'alert-success' : 'alert-danger';
                        const alertMessage = `<div class="alert ${alertClass} fade show" role="alert">
                                        ${response.message}
                                    </div>`;
                        $('#alertContainer').append(alertMessage);

                        setTimeout(function() {
                            $('#alertContainer').fadeOut('slow', function() {
                                $(this).empty().show();
                            });
                        }, 3000);

                        if (response.success) {
                            $('#changePasswordModal').modal('hide');
                        }
                    },
                    error: function() {
                        $('#alertContainer').empty();

                        const alertMessage = `<div class="alert alert-danger fade show" role="alert">
                                        An error occurred while processing your request.
                                    </div>`;
                        $('#alertContainer').append(alertMessage);

                        setTimeout(function() {
                            $('#alertContainer').fadeOut('slow', function() {
                                $(this).empty().show(); 
                            });
                        }, 3000);
                    }
                });
            });

            $('#showPassword').change(function() {
                const passwordFields = ['#currentPassword', '#newPassword', '#confirmPassword'];
                passwordFields.forEach(field => {
                    $(field).attr('type', this.checked ? 'text' : 'password');
                });
            });
        });
    </script>


</body>

</html>