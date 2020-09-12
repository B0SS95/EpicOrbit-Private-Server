namespace EpicOrbit.Shared.ViewModel {
    public class ValidatedView {

        public static ValidatedView Invalid(string message) {
            return new ValidatedView { Message = message };
        }

        public static ValidatedView Valid() {
            return new ValidatedView { IsValid = true };
        }

        public bool IsValid { get; set; }

        public string Message { get; set; }

    }


    public class ValidatedView<T> : ValidatedView {

        public static new ValidatedView<T> Invalid(string message) {
            return new ValidatedView<T> { Message = message };
        }

        public static ValidatedView<T> Valid(T obj) {
            return new ValidatedView<T> { IsValid = true, Object = obj };
        }

        public T Object { get; set; }

    }
}
