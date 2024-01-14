
window.ShowMessage = (type, message) => {
    if (type === 'success') {
        Swal.fire({
            title: "Added Successfully!",
            text: message,
            icon: "success"
        });
    }
    if (type === 'error') {
        Swal.fire({
            title: "Deleted!",
            text: message,
            icon: "success"
        });
    }
    if (type === 'edit') {
        toastr.success(message)
    }

    if (type === 'login') {
        toastr.success(message)
    }


    
}