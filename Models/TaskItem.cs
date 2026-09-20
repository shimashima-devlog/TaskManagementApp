using System.ComponentModel.DataAnnotations;

namespace TaskManagementApp.Models;
public class TaskItem
{
    public int Id{get; set;}

    [Required(ErrorMessage ="タスク名を入力してください。")]
    public required string TaskName{get; set;}

    [Required(ErrorMessage ="担当者名を入力してください。")]
    public required string Assignee{get; set;}

    [Required(ErrorMessage ="期限を入力してください。")]
    [DataType(DataType.Date)]
    public DateTime? DueDate{get; set;}
    public WorkStatus Status{get; set;}
    public string? Notes{get; set;}
    
}

public enum WorkStatus
{
    NotStarted,
    InProgress,
    Completed
}

