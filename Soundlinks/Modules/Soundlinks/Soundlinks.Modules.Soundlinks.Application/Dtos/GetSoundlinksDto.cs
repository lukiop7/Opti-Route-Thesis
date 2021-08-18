using System;
using System.Collections.Generic;
using System.Text;

namespace Soundlinks.Modules.Soundlinks.Application.Dtos
{
    /// <summary>
    /// The DTO for get soundlinks action
    /// </summary>
    public class GetSoundlinksDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        /// <value>
        /// The artist.
        /// </value>
        public string Artist { get; set; }
    }

}
