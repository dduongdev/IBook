function toogleParameterUrl(param, value) {
    const url = new URL(window.location.href);
    const params = new URLSearchParams(url.search);

    // Kiểm tra xem tham số đã có chưa
    if (params.has(param)) {
        // Nếu tham số đã có, thay đổi giá trị của nó
        params.set(param, value);
    } else {
        // Nếu tham số chưa có, thêm tham số mới vào URL
        params.append(param, value);
    }

    // Cập nhật lại URL
    window.location.href = `${url.pathname}?${params}`;
}
