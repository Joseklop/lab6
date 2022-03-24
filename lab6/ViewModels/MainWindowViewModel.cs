using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using lab6.Models;
using System.Collections.ObjectModel;

namespace lab6.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        public List<Note> ItemsAll { get; set; }
        public MainWindowViewModel()
        {
            Content = fv = new ToDoListViewModel(BuildAllNotes());


            AddButton = ReactiveCommand.Create<Unit, Unit>(
                (unit) =>
                {
                    var newItem = new Note("", "", fv.Currentdate);
                    var sv = new NoteViewModel(newItem);
                    Observable.Merge(
                sv.OKButton,
                sv.CancelButton.Select(_ => Unit.Default))
                .Take(1)
                .Subscribe(unit =>
                {
                    if (newItem.Name != "")
                        ItemsAll.Add(newItem);
                    fv.changeItems();
                    Content = fv;
                });
                    Content = sv;
                    return Unit.Default;
                });

            OpenButton = ReactiveCommand.Create<Note, Unit>(
                (item) =>
                {
                    var sv = new NoteViewModel(item);
                    Observable.Merge(
                sv.OKButton,
                sv.CancelButton.Select(_ => Unit.Default))
                .Take(1)
                .Subscribe(unit =>
                {
                    fv.changeItems();
                    Content = fv;
                });
                    Content = sv;
                    return Unit.Default;
                });

            DeleteButton = ReactiveCommand.Create<Note, Unit>((item) =>
            {
                ItemsAll.Remove(item);
                fv.changeItems();
                return Unit.Default;
            });
            ItemsAll = BuildAllNotes();
            Content = fv = new ToDoListViewModel(ItemsAll);


        }

        ViewModelBase content;
        public ViewModelBase Content
        {
            set => this.RaiseAndSetIfChanged(ref content, value);
            get => content;
        }

        public ToDoListViewModel fv { get; }
        public ReactiveCommand<Unit, Unit> AddButton { get; }
        public ReactiveCommand<Note, Unit> OpenButton { get; }
        public ReactiveCommand<Note, Unit> DeleteButton { get; }


        private List<Note> BuildAllNotes()
        {
            return new List<Note>
            {
                new Note("Заголовок Дела 1", "ToDo1", DateTime.Today),
                new Note("Заголовок Дела 2", "ToDo2", DateTime.Today),
                new Note("Заголовок Дела 3", "ToDo3", DateTime.Today.AddDays(1)),
                new Note("Заголовок Дела 4", "ToDo4", DateTime.Today.AddDays(1)),
            };
        }

    }
}