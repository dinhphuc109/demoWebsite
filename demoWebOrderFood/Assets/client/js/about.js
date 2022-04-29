
function change(type, element) {
    let tabs = document.getElementsByClassName('header__navbar-item');
    for (var i = 0; i < tabs.length; i++) {
        tabs[i].style.color = '#fff';
        tabs[i].style.transform = 'translateX(0)';
    }
    element.style.color = '#fff';
    element.style.transform = 'translateX(5px)';
    document.getElementById(type).style.display = 'block';
    switch (type) {
        case 'theFirst':
            document.getElementById('theSecond').style.display = 'none';
            document.getElementById('theThird').style.display = 'none';
            document.getElementById('theFourth').style.display = 'none';
            document.getElementById('website').style.display = 'none';
            break;
        case 'theSecond':
            document.getElementById('theFirst').style.display = 'none';
            document.getElementById('theFourth').style.display = 'none';
            document.getElementById('theThird').style.display = 'none';
            document.getElementById('website').style.display = 'none';
            break;
        case 'theThird':
            document.getElementById('theSecond').style.display = 'none';
            document.getElementById('theFourth').style.display = 'none';
            document.getElementById('theFirst').style.display = 'none';
            document.getElementById('website').style.display = 'none';
            break;
        case 'theFourth':
            document.getElementById('theSecond').style.display = 'none';
            document.getElementById('theThird').style.display = 'none';
            document.getElementById('theFirst').style.display = 'none';
            document.getElementById('website').style.display = 'none';
            break;
        case 'website':
            document.getElementById('theSecond').style.display = 'none';
            document.getElementById('theThird').style.display = 'none';
            document.getElementById('theFirst').style.display = 'none';
            document.getElementById('theFourth').style.display = 'none';
            break;
    }
}