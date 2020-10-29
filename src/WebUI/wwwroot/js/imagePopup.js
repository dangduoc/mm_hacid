$(function () {
    images = [
        "https://i0.wp.com/sociallover.net/wp-content/uploads/2017/05/cute-images.png",
        "https://images.pexels.com/photos/39317/chihuahua-dog-puppy-cute-39317.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500",
        "https://c.wallhere.com/photos/76/a5/anime_anime_girls_digital_art_artwork_2D_portrait_display_vertical_Kochou_Shinobu-1660961.jpg!d",
        "https://toplist.vn/images/800px/trang-web-xem-anime-online-hay-nhat-hien-nay-25269.jpg",
        "https://toplist.vn/images/800px/trang-web-xem-anime-online-hay-nhat-hien-nay-25269.jpg",
        "https://toplist.vn/images/800px/trang-web-xem-anime-online-hay-nhat-hien-nay-25269.jpg"
    ];
    let imagesHtml = "";
    images.forEach(element => {
        imagesHtml += `<div class="image_box"><img src="${element}"></div>`;
    });
    let html = `<div class="gallery_container">${imagesHtml}</div>`;
    let selected_image = '';

    $.confirm({
        title: 'Title',
        content: html,
        onContentReady: function () {
            // when content is fetched & rendered in DOM
            alert('onContentReady');
            var self = this;
            this.buttons.ok.disable();
            this.$content.find('.image_box').click(function () {
                this.$content.find('.image_box').removeClass('selected');
                $(this).addClass('selected');
                self.buttons.ok.enable();
            });
        },
        onAction: function (btnName) {
            if (btnName === 'ok') {
                window.open(this.$content.find('.image_box.selected')[0]);
            }
        }
    });
});