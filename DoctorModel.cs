using System.ComponentModel.DataAnnotations;

public class Doctor
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [StringLength(100)]
    public string Specialization { get; set; }

    [Required]
    [StringLength(20)]
    public string Phone { get; set; }

    public bool IsAvailable { get; set; } = true;

    // Navigation property
    public virtual ICollection<Appointment> Appointments { get; set; }
}
