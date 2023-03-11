
namespace EIF_VIKO_TVARKARASTIS_REST_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddDbContext<DataContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            var teachers = new List<Teacher>
            {
                new Teacher{Id = 1, Name ="Marius", UniqueID=-120 },
                                new Teacher{Id = 2, Name ="Petras", UniqueID=-155 }

            };


            app.MapGet("/teachers", async (DataContext context) => 
                await context.Teachers.ToListAsync());

            app.MapGet("/teacher/{id}", async (DataContext context, int id) =>
                await context.Teachers.FindAsync(id) is Teacher teacher ? 
                Results.Ok(teacher) : 
                Results.NotFound("Sorry, this teacher doesn't exists."));

            app.MapPost("/teacher", async (DataContext context, Teacher teacher) =>
            {
                context.Teachers.Add(teacher);
                await context.SaveChangesAsync();
                return Results.Ok(await context.Teachers.ToListAsync());
            });

            app.MapPut("/teacher/{id}", async(DataContext context, Teacher updatedTeacher, int id) =>
            {
                var teacher = await context.Teachers.FindAsync(id);

                if (teacher is null)
                    return Results.NotFound("Sorry, this teacher doesn't exists.");
                teacher.Name = updatedTeacher.Name;
                teacher.UniqueID = updatedTeacher.UniqueID;
                await context.SaveChangesAsync();
                
                return Results.Ok(teacher);
            });

            app.MapDelete("/teacher/{id}", async (DataContext context, int id) =>
            {
                var teacher = await context.Teachers.FindAsync(id); 
                if (teacher is null)
                    return Results.NotFound("Sorry, this teacher doesn't exists.");
                context.Teachers.Remove(teacher);
                await context.SaveChangesAsync(); 
                return Results.Ok(await context.Teachers.ToListAsync());
            });
          





            app.Run();
        }
    }

    public class Teacher
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int UniqueID { get; set; }
    }
}