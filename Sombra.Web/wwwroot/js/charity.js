
//TODO
//This complete loop needs to get filled with info from the backend
//for (x = 0; x < 11; x++) {
//    // Make progress bar
//    var carousel_progress = document.createElement('div');
//    carousel_progress.className = "carousel-progress";
//    var carousel_progress_bar = document.createElement('div');
//    carousel_progress_bar.className = "carousel-progress-bar";
//    carousel_progress_bar.appendChild(carousel_progress);
//    // Make logo
//    var carousel_logo = document.createElement('div');
//    carousel_logo.className = "carousel-logo";
//    // Make backgrond
//    var carousel_background = document.createElement('div');
//    carousel_background.className = "carousel-background";
//    carousel_background.appendChild(carousel_progress_bar);
//    carousel_background.appendChild(carousel_logo);
//    // Make Moneyholder
//    var carousel_money_holder = document.createElement('div');
//    carousel_money_holder.className = "carousel-money-holder";
//    var reached = document.createElement('label');
//    reached.className = "carousel-reached";
//    reached.innerHTML = "<span style=\"font-family: 'Source Sans Pro', sans-serif;\">&euro; 6.800</span>";
//    var goal = document.createElement('label');
//    goal.className = "carousel-goal";
//    goal.innerHTML = "<span style=\"font-family: 'Source Sans Pro', sans-serif;\">behaald van &euro; 15.000</span>";
//    carousel_money_holder.appendChild(reached);
//    carousel_money_holder.appendChild(goal);
//    // Make Body
//    var carousel_body = document.createElement('div');
//    carousel_body.className = "carousel-body";
//    var label = document.createElement('label');
//    label.className = "carousel-date";
//    var date = new Date();
//    label.innerText = date.getDate() + "/" + date.getMonth() + "/" + date.getFullYear();
//    var title = document.createElement('h4');
//    title.className = "carousel-title";
//    title.innerHTML = "Klimmen tegen MS &#64; Mont Ventoux";
//    var img = document.createElement('img');
//    img.className = "carousel-poster-image";
//    img.src = "Assets/lianne.jpg";
//    var labelimg = document.createElement('label');
//    labelimg.className = "carousel-poster-name";
//    labelimg.innerHTML = "<span style=\"font-family: 'Source Sans Pro', sans-serif;\">Fabiene ter Beek</span>";
//    carousel_body.appendChild(label);
//    carousel_body.appendChild(title);
//    carousel_body.appendChild(img);
//    carousel_body.appendChild(labelimg);
//    carousel_body.appendChild(carousel_money_holder);
//    // Make container
//    var carousel_container = document.createElement('div');
//    carousel_container.className = "carousel-container";
//    carousel_container.appendChild(carousel_background);
//    carousel_container.appendChild(carousel_body);
//    // Make cell
//    var carousel_cell = document.createElement('div');
//    carousel_cell.className = "carousel-cell";
//    carousel_cell.appendChild(carousel_container);
//    // Add to View
//    document.getElementById('main-carousel').appendChild(carousel_cell);
//}