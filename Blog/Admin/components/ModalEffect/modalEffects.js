/**
 * modalEffects.js v1.0.0
 * http://www.codrops.com
 *
 * Licensed under the MIT license.
 * http://www.opensource.org/licenses/mit-license.php
 * 
 * Copyright 2013, Codrops
 * http://www.codrops.com
 */
var ModalEffects = (function() {

	function init() {

		var overlay = $('.md-overlay' )[0];

		[].slice.call( $( '.md-trigger' ) ).forEach( function( el, i ) {

		    var modal = $('#modal-1'),
				close = $(modal).find( '.md-close' )[0];

			function removeModal( hasPerspective ) {
				classie.remove( modal, 'md-show' );

				if( hasPerspective ) {
					classie.remove( document.documentElement, 'md-perspective' );
				}
			}

			function removeModalHandler() {
				removeModal( classie.has( el, 'md-setperspective' ) ); 
			}

			$(el).bind('click', function (ev)
			{
			    $(modal).addClass('md-show')
			    $(overlay).unbind( 'click', removeModalHandler );
			    $(overlay).bind('click', removeModalHandler);

				if( classie.has( el, 'md-setperspective' ) ) {
					setTimeout( function() {
						classie.add( document.documentElement, 'md-perspective' );
					}, 25 );
				}
			});

			$(close).on( 'click', function( ev ) {
				ev.stopPropagation();
				removeModalHandler();
			});

		} );

	}

	init();

})();