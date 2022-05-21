using System;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyTodoApp.Models
{
    public class Todo
    {
        public int Id {get; set;}

        [DisplayName ("Título")]// Atributos para tela quando gerar o codigo e a tela endender esses atributos 
        //e renderizar a tela e na hora de gerar o banco. Tambem existe outras coisas que pode fazer também!
        [Required(ErrorMessage = "Campo obnrigatório")] // somente required deixa uma mensagem em ingles
        //errormessage sobrescreve a mensagem em ingles
        public string Title {get; set;} // se deixar apenas Title, o codigo vai deixar em inglês a tela

        [DisplayName("Concluído")]
        public bool Done {get; set;}
      
        [DisplayName("Criado em")]
        public DateTime CreatedAt{get; set;}=DateTime.Now;

        [DisplayName("{Última atualização")]
        public DateTime LastUpdateDate {get; set;} = DateTime.Now;
        
        [DisplayName("Usuário")]
        public string User {get; set;}
    }
}