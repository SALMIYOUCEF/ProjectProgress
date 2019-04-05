    $(function() {
        
        var scrollY = function() {
            var supportPageOffset = window.pageXOffset !== undefined;
            var isCSS1Compat = ((document.compatMode || "") === "CSS1Compat");
            return supportPageOffset ? window.pageYOffset : isCSS1Compat ? document.documentElement.scrollTop : document.body.scrollTop;
        }

        var element = document.querySelector("#filterTable");
        var rect = element.getBoundingClientRect();
        var top = rect.top + scrollY();
        var width = rect.width;
      
        // functions
        var onScroll = function() {
        var hasScrollClass = element.classList.contains("fixed");
        if (scrollY() > top && !hasScrollClass) {
              element.classList.add("fixed");
              element.style.width = width+ "px";
           } else if(scrollY() < top && hasScrollClass){
              element.classList.remove("fixed");
           }
        }
        
        var onResize = function() {
            element.style.width = "auto";
            element.classList.remove("fixed");
            rect = element.getBoundingClientRect();
            top = rect.top + scrollY();
            width = rect.width;
            onScroll();
        }
        // listener
        window.addEventListener('scroll',onScroll);
        window.addEventListener('resize',onResize);
    });

$(function() {
    $( "#sortable" ).sortable();
    $( "#sortable" ).disableSelection();
} );